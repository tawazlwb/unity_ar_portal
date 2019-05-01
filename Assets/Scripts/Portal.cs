using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class Portal : MonoBehaviour
{
    public Material[] materials;

    public Transform device;

    bool wasInFront;
    bool inOtherWorld;
    
    // Start is called before the first frame update
    void Start()
    {
        SetMaterials(false);
    }

    void SetMaterials(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;
        
        foreach (var material in materials)
        {
            material.SetInt("_StencilTest", (int)stencilTest);
        }
    }

    bool IsInFront()
    {
        Vector3 pos = transform.InverseTransformPoint(device.position);
        return pos.z >= 0 ? true : false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != device)
            return;

        wasInFront = IsInFront();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform != device)
            return;

        bool isInFront = IsInFront();
        if ((isInFront && !wasInFront) || (!isInFront && wasInFront))
        {
            inOtherWorld = !inOtherWorld;
            SetMaterials(inOtherWorld);
        }

        wasInFront = isInFront;
    }

    private void OnDestroy()
    {
        SetMaterials(true);
    }

    // Update is called once per frame
    void Update(){
    }
}

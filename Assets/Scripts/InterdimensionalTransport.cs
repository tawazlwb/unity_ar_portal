using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class InterdimensionalTransport : MonoBehaviour
{
    public Material[] materials;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var material in materials)
        {
            material.SetInt("_StencilTest", (int)CompareFunction.Equal);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("MainCamera"))
            return;

        // Outside of other world
        if(transform.position.z > other.transform.position.z)
        {
            Debug.Log("Outside other world");
            foreach ( var material in materials) {
                material.SetInt("_StencilTest", (int) CompareFunction.Equal);
            }
        }
        // Inside of other world
        else
        {
            Debug.Log("Inside other world");
            foreach (var material in materials)
            {
                material.SetInt("_StencilTest", (int) CompareFunction.NotEqual);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var material in materials)
        {
            material.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

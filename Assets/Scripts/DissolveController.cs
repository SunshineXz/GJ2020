using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// countdown of property dissolve time from up to 1 to -1
/// </summary>
public class DissolveController : MonoBehaviour
{
    public Renderer rend;
    
    private Material dissolveMat;
    
    [Range(0,1)]
    public float dissolveRate;
    
    private float currentDissolve;
    
    [Range(-1,1)]
    public float minValue;
    
    [Range(-1,1)]
    public float maxValue;

    void Start()
    {
        dissolveMat = rend.material;
        currentDissolve = maxValue;
        // minValue /= 100;
        // maxValue /= 100;
        // dissolveRate /= 100;
        dissolveMat.SetFloat("Vector1_3F3EEC92",1f);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pressed space");
            StepDissolve();
        }
    }

    void StepDissolve()
    {
        if (currentDissolve >= minValue)
        {
            Debug.Log("resolving space"+ dissolveMat.GetFloat("Vector1_3F3EEC92"));

            currentDissolve -= dissolveRate;
            rend.material.SetFloat("Vector1_3F3EEC92",currentDissolve);
        }
    }
}

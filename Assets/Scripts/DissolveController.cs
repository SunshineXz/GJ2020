using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public Material dissolveMat;
    public float value;
    public float maxValue;

    void Start()
    {
        dissolveMat.SetFloat("", value / maxValue);
    }       
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePartController : MonoBehaviour
{
    private bool isSpawning = false;
    private Material m;
    public float spawnTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // SOME SHENANIGAN STUFF RIGHT HERE.
        GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);

        m = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawning)
        {
            GetComponent<Renderer>().material.SetColor("Emissive_core", GetComponent<Renderer>().material.GetColor("Emissive_core") * 1.01f);
            GetComponent<Renderer>().material.SetFloat("Alpha_core", GetComponent<Renderer>().material.GetFloat("Alpha_core") + Time.deltaTime);
            isSpawning = GetComponent<Renderer>().material.GetFloat("Alpha_core") < 1;
        }
    }

    public void SpawnItem()
    {
        isSpawning = true;
    }

    public void SetAlphaToZero()
    {
        m.SetFloat("Alpha_core", 0);
    }
}

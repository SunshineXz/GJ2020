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
        // set it transparent at the begining
        m.SetFloat("Alpha", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawning)
        {
            m.SetFloat("Alpha", m.GetFloat("Alpha") + Time.deltaTime);
            isSpawning = m.GetFloat("Alpha") < 1;
        }
    }

    public void SpawnItem()
    {
        isSpawning = true;
    }
}

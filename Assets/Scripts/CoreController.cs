using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    public List<GameObject> coreParts;

    private int currentIndex = 0;

    private float rotationSpeed = 75.0f;

    // Start is called before the first frame update
    void Start()
    {
        coreParts.ForEach(x => x.GetComponent<CorePartController>().SetAlphaToZero());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            AddObject();
    }

    public void AddObject()
    {
        if(coreParts.Count > currentIndex)
        {
            // 2 at the same time.
            coreParts[currentIndex].GetComponent<CorePartController>().SpawnItem();
            currentIndex++;
            coreParts[currentIndex].GetComponent<CorePartController>().SpawnItem();
            currentIndex++;
        }
    }
}

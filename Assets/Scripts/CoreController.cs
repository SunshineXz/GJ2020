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
    }

    // Update is called once per frame
    void Update()
    {
        // For now
        //transform.Rotate(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.B))
            AddObject();
    }

    public void AddObject()
    {
        if(coreParts.Count > currentIndex)
        {
            coreParts[currentIndex].GetComponent<CorePartController>().SpawnItem();
            currentIndex++;
        }
    }
}

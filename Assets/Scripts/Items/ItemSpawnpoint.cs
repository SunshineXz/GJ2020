using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ItemSpawnpoint : MonoBehaviour
{
    public bool isRepair = false;
    public List<GameObject> items;
    public Item currentItem;

    public float baseWaitItem = 1.0f;
    public float maxExtraWait = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetRandomItem());
    }

    private IEnumerator SetRandomItem()
    {
        Random random = new Random();
        //wait from 5 to 10 seconds
        yield return new WaitForSeconds(random.Next((int)Mathf.Round(maxExtraWait * 10))/10.0f + baseWaitItem);
        var index = random.Next(items.Count);
        Item item = Instantiate(items[index], transform).GetComponent<Item>();
        currentItem = item;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentItem != null)
        {
            currentItem.Pickup(other.GetComponent<ItemController>());
            StartCoroutine(SetRandomItem());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RepairSpawnPoint : MonoBehaviour
{
    public GameObject repairItem;
    public RepairItem currentItem;

    public void SpawnItem()
    {
        currentItem = Instantiate(repairItem, transform).GetComponent<RepairItem>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentItem != null)
        {
            currentItem.Pickup(other.GetComponent<ItemController>());
        }
    }
}

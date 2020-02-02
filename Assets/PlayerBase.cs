using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public GameObject owner;

    private void OnTriggerEnter(Collider other)
    {
        var GO = other.gameObject;
        if(GO == owner)
        {
            var controller = other.GetComponent<ItemController>();
            if (controller.currentRepairItem != null)
            {
                GameManager.Instance.ScorePoint(GO.GetComponent<CharacterMovementController>().playerID);
                Destroy(controller.currentRepairItem.gameObject);
                controller.repairImage.sprite = null;
                controller.currentRepairItem = null;
            }
        }
    }
}

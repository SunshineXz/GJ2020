using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public RepairItem currentRepairItem;
    public EffectItem currentEffectItem;

    public ItemRecharge itemRecharge;
    public Image repairImage;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            DropRepairItem();
        }
        if(Input.GetKey(KeyCode.E))
        {
            UseItem();
        }
        repairImage.enabled = repairImage.sprite != null;
    }

    private void UseItem()
    {
        if(currentEffectItem != null)
        {
            currentEffectItem.Use(GetComponent<CharacterMovementController>());
        }
    }

    public bool PickUpRepairItem(RepairItem item)
    {
        bool canPickUp = currentRepairItem == null;
        if(canPickUp)
        {
            currentRepairItem = item;
            repairImage.sprite = item.sprite;
        }
        return canPickUp;
    }

    public void PickUpEffectItem(EffectItem item)
    {
        currentEffectItem = item;
        itemRecharge.SetItem(item);
    }

    public void DropRepairItem()
    {
        if(currentRepairItem != null)
        {
            currentRepairItem.transform.position = transform.position - transform.forward * 5;
            currentRepairItem.gameObject.SetActive(true);
            currentRepairItem.SetColliderActive(true);
            repairImage.sprite = null;
            currentRepairItem = null;
        }
    }
}

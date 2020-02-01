using UnityEngine;


public class RepairItem : Item
{
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var itemController = other.GetComponent<ItemController>();
            if (itemController.currentRepairItem == null)
            {
                itemController.PickUpRepairItem(this);
                Destroy();
            }
        }
    }
}
using UnityEngine;


public class RepairItem : Item
{
    public override void Pickup(ItemController itemController)
    {
        if(itemController.PickUpRepairItem(this))
        {
            Destroy();
        }
    }
}
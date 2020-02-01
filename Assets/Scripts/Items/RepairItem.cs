using UnityEngine;


public class RepairItem : Item
{
    private BoxCollider coll;

    public void Start()
    {
        coll = GetComponent<BoxCollider>();
        SetColliderActive(false);
    }

    public override void Pickup(ItemController itemController)
    {
        if(itemController.PickUpRepairItem(this))
        {
            Destroy();
        }
    }

    public void SetColliderActive(bool active)
    {
        coll.enabled = active;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other.GetComponent<ItemController>());
            Destroy();
        }
    }
}
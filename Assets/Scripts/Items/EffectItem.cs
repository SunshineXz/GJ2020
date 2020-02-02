using UnityEngine;


public abstract class EffectItem : Item
{
    GameObject Prefab;

    public abstract void Use(CharacterMovementController characterMovementController);

    public override void Pickup(ItemController itemController)
    {
        itemController.PickUpEffectItem(this);
        Destroy();
        FindObjectOfType<AudioManager>().Play("Item");
    }
}
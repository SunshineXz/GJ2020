using UnityEngine;


public abstract class EffectItem : Item
{
    GameObject Prefab;

    public abstract void Use(CharacterMovementController characterMovementController);

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ItemController>().PickUpEffectItem(this);
            Destroy();
        }
    }
}
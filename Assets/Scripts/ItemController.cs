using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public RepairItem currentRepairItem;
    public EffectItem currentEffectItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            DropRepairItem();
        }
        if(Input.GetKey(KeyCode.E))
        {
            currentEffectItem.Use(GetComponent<CharacterMovementController>());
        }
    }

    private void UseItem()
    {
        if(currentEffectItem != null)
        {
            //currentEffectItem.Use();
            currentEffectItem = null;
        }
    }

    public void PickUpRepairItem(RepairItem item)
    {
        currentRepairItem = item;
    }

    public void PickUpEffectItem(EffectItem item)
    {
        currentEffectItem = item;
    }

    public void DropRepairItem()
    {
        if(currentRepairItem != null)
        {
            currentRepairItem.transform.position = transform.position + transform.forward * 2;
            currentRepairItem.gameObject.SetActive(true);
            currentRepairItem = null;
        }
    }
}

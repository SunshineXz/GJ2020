using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecharge : MonoBehaviour
{
    public Image itemImage;
    public Image maskImage;

    private bool used = false;
    private float cooldownDuration = 5f;
    private float nextReadyTime;
    private float cooldownTimeLeft;

    private void Update()
    {
        if(used)
        {
            bool cooldownComplete = (Time.time > nextReadyTime);
            if (!cooldownComplete)
            {
                cooldownTimeLeft -= Time.deltaTime;
                float roundedCd = Mathf.Round(cooldownTimeLeft);
            }
            else
            {
                used = false;
                itemImage.sprite = null;
            }
            maskImage.fillAmount = GetReadyNormalized();
        }
        itemImage.enabled = itemImage.sprite != null;
    }

    public void SetItem(Item item)
    {
        itemImage.sprite = item?.sprite;
    }

    public float GetReadyNormalized()
    {
        return cooldownTimeLeft / cooldownDuration;
    }

    public void PutOnCooldown(float cooldown)
    {
        used = true;
        cooldownDuration = cooldown;
        nextReadyTime = cooldownDuration + Time.time;
        cooldownTimeLeft = cooldownDuration;
    }
}
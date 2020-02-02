using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public Sprite sprite;

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public abstract void Pickup(ItemController itemController);
}

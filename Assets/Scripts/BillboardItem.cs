﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}

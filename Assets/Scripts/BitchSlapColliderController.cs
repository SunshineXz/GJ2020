﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitchSlapColliderController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player") && collider.gameObject != gameObject.transform.parent.gameObject)
        {
            collider.gameObject.GetComponent<CharacterMovementController>().ReceiveBitchSlap();
        }
    }
}

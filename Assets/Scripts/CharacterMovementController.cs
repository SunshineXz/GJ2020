using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject projectilePrefab;
    public float stunTime = 2.0f;

    bool isStunned = false;
    float stunnedtimer;

    public float movementSpeed = 4.0f;
    public float rotationSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetButtonDown("Fire1"))
        {
            var projectile = Instantiate(projectilePrefab, transform);

            projectile.GetComponent<SpitController>().shooter = gameObject;
            projectile.transform.parent = null;
        }
    }

    private void MovePlayer()
    {
        if(!isStunned)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            controller.Move(new Vector3(x, 0, y) * Time.deltaTime * movementSpeed);
        }
        else
        {
            isStunned = Time.time  >= stunnedtimer;
        }

        float rx = Input.GetAxis("HorizontalRotation");
        float ry = Input.GetAxis("VerticalRotation");

        if(rx != 0 || ry != 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(rx, 0, ry) * Time.deltaTime, transform.up), rotationSpeed);
    }

    void OnTriggerEnter(Collider collider)
    {
        //if(collider.CompareTag("Spit") && collider.gameObject.GetComponent<SpitController>().shooter != gameObject)
        //{
        //    // STUN THIS BITCH
        //    isStunned = true;
        //    stunnedtimer = Time.time + stunTime;
        //}
    }
}

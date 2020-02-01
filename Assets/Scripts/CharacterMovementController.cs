using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public bool canMove = true;
    private CharacterController controller;

    //Prefab
    public GameObject projectilePrefab;
    public GameObject bitchSlapCollider;

    //Objects
    public GameObject shieldObject;
    public ParticleSystem infiniteShootParticleSystem;
    public ParticleSystem boostParticleSystem;

    //Stun
    public float stunTime = 2.0f;
    private bool isStunned = false;
    private float stunnedtimer;
    public bool shieldUp = false;

    //Shoot
    public bool infiniteShoot = false;
    public float shootTimer = 0.0f;
    public float shootCooldown = 2.0f;

    //Movement
    public float movementSpeed = 4.0f;
    public float NORMAL_MOVEMENT_SPEED = 10.0f;
    public float rotationSpeed = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        bitchSlapCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetButtonDown("Spit"))
        {
            var canShoot = Time.time >= shootTimer || infiniteShoot;

            if(canShoot)
            {
                var projectile = Instantiate(projectilePrefab, transform);

                projectile.GetComponent<SpitController>().shooter = gameObject;
                projectile.transform.parent = null;

                shootTimer = Time.time + shootCooldown;
            }
        }

        if(Input.GetButtonDown("BitchSlap"))
        {
            bitchSlapCollider.SetActive(true);
            StartCoroutine(WaitToSlap());
        }
    }

    private void MovePlayer()
    {
        if(!isStunned && canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            controller.Move(new Vector3(x, 0, y) * Time.deltaTime * movementSpeed);
        }
        else
        {
            isStunned = Time.time  <= stunnedtimer;
        }

        float rx = Input.GetAxis("HorizontalRotation");
        float ry = Input.GetAxis("VerticalRotation");

        if(rx != 0 || ry != 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(rx, 0, ry) * Time.deltaTime, transform.up), rotationSpeed);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Spit") && collider.gameObject.GetComponent<SpitController>().shooter != gameObject)
        {
            //if no protection -> stunned
            if(!shieldUp)
            {
                // STUN THIS BITCH
                StunPlayer();
            }
        }
    }

    public void SetMovementSpeed(float movementSpeedMultiplier, float time)
    {
        movementSpeed *= movementSpeedMultiplier;
        boostParticleSystem.Play();
        StartCoroutine(ResetMovementSpeed(time));
    }

    private IEnumerator ResetMovementSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        movementSpeed = NORMAL_MOVEMENT_SPEED;
        boostParticleSystem.Stop();
    }

    public void SetShieldUp(float time)
    {
        shieldUp = true;
        shieldObject.SetActive(true);
        StartCoroutine(ShieldDown(time));
    }

    private IEnumerator ShieldDown(float time)
    {
        yield return new WaitForSeconds(time);
        shieldUp = false;
        shieldObject.SetActive(false);
    }

    public void SetInfiniteShoot(float time)
    {
        infiniteShoot = true;
        infiniteShootParticleSystem.Play();
        StartCoroutine(InfiniteShootDown(time));
    }

    private IEnumerator InfiniteShootDown(float time)
    {
        yield return new WaitForSeconds(time);
        infiniteShoot = false;
        infiniteShootParticleSystem.Stop();
    }

    public void ReceiveBitchSlap()
    {
        StunPlayer();
    }

    public void StunPlayer()
    {
        isStunned = true;
        stunnedtimer = Time.time + stunTime;
    }

    IEnumerator WaitToSlap()
    {
        yield return new WaitForSeconds(0.5f);
        bitchSlapCollider.SetActive(false);
    }
}

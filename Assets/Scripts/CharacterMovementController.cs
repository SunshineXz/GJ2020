using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public bool canMove = true;
    private CharacterController controller;
    private Animator anim;

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
    public InputManager inputManager;
    public string playerName;
    public float movementSpeed = 4.0f;
    public float NORMAL_MOVEMENT_SPEED = 10.0f;
    public float rotationSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        bitchSlapCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (inputManager.GetAxisOrButtonDown("Spit", playerName))
        {
            var canShoot = Time.time >= shootTimer || infiniteShoot;

            if(canShoot)
            {
                anim.SetTrigger("Shoot");
                var projectile = Instantiate(projectilePrefab, transform);

                projectile.GetComponent<SpitController>().shooter = gameObject;
                projectile.transform.parent = null;

                shootTimer = Time.time + shootCooldown;
            }
        }

        if(inputManager.GetAxisOrButtonDown("BitchSlap", playerName))
        {
            bitchSlapCollider.SetActive(true);
            StartCoroutine(WaitToSlap());
        }
    }

    private void MovePlayer()
    {
        if(!isStunned && canMove)
        {
            float x = inputManager.GetAxis("Horizontal", playerName);
            float y = inputManager.GetAxis("Vertical", playerName);

            var walkMagnitude = new Vector2(x, y).SqrMagnitude();
            anim.SetFloat("Walk", walkMagnitude);
            controller.Move(new Vector3(x, 0, y) * Time.deltaTime * movementSpeed);
        }
        else
        {
            anim.SetFloat("Walk", 0.0f);
            isStunned = Time.time  <= stunnedtimer;
        }

        float rx = inputManager.GetAxis("HorizontalRotation", playerName);
        float ry = inputManager.GetAxis("VerticalRotation", playerName);

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
                StunPlayer(GlobalVariables.GlobalVariablesInstance.SHOOT_STUN_DURATION);
            }
        }
    }

    public void SetMovementSpeedBoost(float movementSpeedMultiplier, float time, GameObject itemToDestroy)
    {
        movementSpeed *= movementSpeedMultiplier;
        boostParticleSystem.Play();
        StartCoroutine(ResetMovementSpeed(time, itemToDestroy));
    }

    private IEnumerator ResetMovementSpeed(float time, GameObject itemToDestroy)
    {
        yield return new WaitForSeconds(time);
        movementSpeed = NORMAL_MOVEMENT_SPEED;
        boostParticleSystem.Stop();
        Destroy(itemToDestroy);
    }

    public void SetShieldUp(float time, GameObject itemToDestroy)
    {
        shieldUp = true;
        shieldObject.SetActive(true);
        StartCoroutine(ShieldDown(time, itemToDestroy));
    }

    private IEnumerator ShieldDown(float time, GameObject itemToDestroy)
    {
        yield return new WaitForSeconds(time);
        shieldUp = false;
        shieldObject.SetActive(false);
        Destroy(itemToDestroy);
    }

    public void SetInfiniteShoot(float time, GameObject itemToDestroy)
    {
        infiniteShoot = true;
        infiniteShootParticleSystem.Play();
        StartCoroutine(InfiniteShootDown(time, itemToDestroy));
    }

    private IEnumerator InfiniteShootDown(float time, GameObject itemToDestroy)
    {
        yield return new WaitForSeconds(time);
        infiniteShoot = false;
        infiniteShootParticleSystem.Stop();
        Destroy(itemToDestroy);
    }

    public void ReceiveBitchSlap()
    {
        StunPlayer(GlobalVariables.GlobalVariablesInstance.SLAP_STUN_DURATION);
    }

    public void StunPlayer(float duration)
    {
        isStunned = true;
        stunnedtimer = Time.time + duration;
    }

    IEnumerator WaitToSlap()
    {
        yield return new WaitForSeconds(0.5f);
        bitchSlapCollider.SetActive(false);
    }
}

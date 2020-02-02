using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public bool canMove = true;
    private CharacterController controller;
    private Animator anim;
    private Player player;

    //Prefab
    public GameObject projectilePrefab;
    public GameObject bitchSlapCollider;

    //Objects
    public GameObject shieldObject;
    public ParticleSystem infiniteShootParticleSystem;
    public ParticleSystem boostParticleSystem;
    public ParticleSystem stunParticleSystem;

    //Stun
    private bool isStunned = false;
    private float stunnedtimer;
    public bool shieldUp = false;
    public float stunCooldown;

    //Shoot
    public bool infiniteShoot = false;
    public float shootTimer = 0.0f;

    //Movement
    public int playerID;
    public float movementSpeed = 4.0f;
    public float NORMAL_MOVEMENT_SPEED = 10.0f;
    public float rotationSpeed = 0.5f;
    private bool canSlap = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        bitchSlapCollider.SetActive(false);
        player = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        // Can't do anything dumb fuck
        if (isStunned)
            return;

        if (player.GetButtonDown("Spit") || player.GetAxis("Spit") > 0)
        {
            if(Time.time >= shootTimer)
            {
                anim.SetTrigger("Shoot");
                var projectile = Instantiate(projectilePrefab, transform);

                projectile.GetComponent<SpitController>().shooter = gameObject;
                projectile.transform.parent = null;
                projectile.transform.localScale = GlobalVariables.GlobalVariablesInstance.SHOOT_BASE_SCALE;

                if(infiniteShoot)
                    shootTimer = Time.time + GlobalVariables.GlobalVariablesInstance.BULLET_TIME_REDUCED_COOLDOWN;
                else
                    shootTimer = Time.time + GlobalVariables.GlobalVariablesInstance.SHOOT_COOLDOWN_TIME;
            }
        }

        if(player.GetButtonDown("BitchSlap") || player.GetAxis("BitchSlap") > 0)
        {
            if(Time.time >= stunCooldown)
            {
                anim.SetTrigger("Slap");
                StartCoroutine(WaitToStunOnSlap());
                StartCoroutine(WaitToFinishSlap());
                stunCooldown = Time.time + GlobalVariables.GlobalVariablesInstance.SLAP_COOLDOWN_TIME;
            }
        }
    }

    private void MovePlayer()
    {
        if(!isStunned && canMove)
        {
            float x = player.GetAxis("Horizontal");
            float y = player.GetAxis("Vertical");

            var walkMagnitude = new Vector2(x, y).SqrMagnitude();
            anim.SetFloat("Walk", walkMagnitude);
            controller.Move(new Vector3(x, 0, y) * Time.deltaTime * movementSpeed);
        }
        else
        {
            anim.SetFloat("Walk", 0.0f);
            isStunned = Time.time  <= stunnedtimer;

            if (!isStunned)
            {
                anim.SetBool("Stunned", false);
                stunParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }

        float rx = player.GetAxis("HorizontalRotation");
        float ry = player.GetAxis("VerticalRotation");

        if(rx != 0 || ry != 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(rx, 0, ry) * Time.deltaTime, transform.up), rotationSpeed);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Spit") && collider.gameObject.GetComponent<SpitController>().shooter != gameObject)
        {
            //if no protection -> slowed
            if(!shieldUp)
            {
                // SLOW THIS BITCH
                SetMovementSpeed(GlobalVariables.GlobalVariablesInstance.SLOW_SPEED_MULTIPLIER, GlobalVariables.GlobalVariablesInstance.SLOW_TIME);
            }
        }
    }

    public void SetMovementSpeedBoost(float movementSpeedMultiplier, float time, GameObject itemToDestroy)
    {
        movementSpeed *= movementSpeedMultiplier;
        boostParticleSystem.Play();
        StartCoroutine(ResetMovementSpeed(time, itemToDestroy));
    }

    public void SetMovementSpeed(float multiplier, float time)
    {
        movementSpeed *= multiplier;
        anim.speed *= (multiplier / 2);
        StartCoroutine(ResetMovementSpeed(time));
    }

    public IEnumerator ResetMovementSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        movementSpeed = GlobalVariables.GlobalVariablesInstance.PLAYER_MOVEMENT_SPEED;
        anim.speed = 1;
    }

    private IEnumerator ResetMovementSpeed(float time, GameObject itemToDestroy)
    {
        yield return new WaitForSeconds(time);
        movementSpeed = GlobalVariables.GlobalVariablesInstance.PLAYER_MOVEMENT_SPEED;
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
        //if no protection -> stunned
        if (!shieldUp)
            StunPlayer(GlobalVariables.GlobalVariablesInstance.SLAP_STUN_DURATION);
    }

    public void StunPlayer(float duration)
    {
        isStunned = true;
        stunnedtimer = Time.time + duration;

        stunParticleSystem.Play();
        anim.SetBool("Stunned", true);
    }

    IEnumerator WaitToStunOnSlap()
    {
        yield return new WaitForSeconds(0.35f);
        bitchSlapCollider.SetActive(true);
    }

    IEnumerator WaitToFinishSlap()
    {
        yield return new WaitForSeconds(0.5f);
        bitchSlapCollider.SetActive(false);
    }
}

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
    private Rigidbody rb;

    //Prefab
    public GameObject projectilePrefab;
    public GameObject bitchSlapCollider;

    //Objects
    public ParticleSystem shieldObject;
    public ParticleSystem infiniteShootParticleSystem;
    public ParticleSystem boostParticleSystem;
    public ItemRecharge itemRecharge;
    public Recharge recharge;
    public ParticleSystem stunParticleSystem;
    public ParticleSystem slamParticleSystem;

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
    public float movementSpeed = 15.0f;
    public float NORMAL_MOVEMENT_SPEED = 15.0f;
    public float rotationSpeed = 0.5f;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        bitchSlapCollider.SetActive(false);
        player = ReInput.players.GetPlayer(playerID);
        rb = GetComponent<Rigidbody>();
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

                FindObjectOfType<AudioManager>().Play("Spit");

                var projectile = Instantiate(projectilePrefab, transform);

                projectile.GetComponent<SpitController>().shooter = gameObject;
                projectile.transform.parent = null;
                projectile.transform.localScale = GlobalVariables.GlobalVariablesInstance.SHOOT_BASE_SCALE;

                var cooldown = 0.0f;
                if(infiniteShoot)
                {
                    cooldown = GlobalVariables.GlobalVariablesInstance.BULLET_TIME_REDUCED_COOLDOWN;
                }
                else
                {
                    cooldown = GlobalVariables.GlobalVariablesInstance.SHOOT_COOLDOWN_TIME;
                }
                shootTimer = Time.time + cooldown;
                recharge.PutOnCooldown(cooldown);
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
                FindObjectOfType<AudioManager>().Play("Attack");
            }

        
        }
    }

    public void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.rotation = rotation;
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
        {
            rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(rx, 0, ry) * Time.deltaTime, transform.up), rotationSpeed);
        }
        else
        {
            rotation = transform.rotation;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Spit") && collider.gameObject.GetComponent<SpitController>().shooter != gameObject)
        {
            if(!shieldUp)
            {
                SetMovementSpeed(GlobalVariables.GlobalVariablesInstance.SLOW_SPEED_MULTIPLIER, GlobalVariables.GlobalVariablesInstance.SLOW_TIME);
                FindObjectOfType<AudioManager>().Play("Damage");
            }
        }
    }

    public void SetMovementSpeedBoost(float movementSpeedMultiplier, float time, GameObject itemToDestroy)
    {
        movementSpeed = GlobalVariables.GlobalVariablesInstance.PLAYER_MOVEMENT_SPEED * movementSpeedMultiplier;
        boostParticleSystem.Play();
        itemRecharge.PutOnCooldown(time);
        StartCoroutine(ResetMovementSpeed(time, itemToDestroy));
        FindObjectOfType<AudioManager>().Play("Boost");
    }

    public void SetMovementSpeed(float multiplier, float time)
    {
        movementSpeed = GlobalVariables.GlobalVariablesInstance.PLAYER_MOVEMENT_SPEED * multiplier;
        anim.speed = (multiplier / 2);
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
        GetComponent<ItemController>().currentEffectItem = null;
    }

    public void SetShieldUp(float time, GameObject itemToDestroy)
    {
        shieldUp = true;
        shieldObject.Play();
        itemRecharge.PutOnCooldown(time);
        StartCoroutine(ShieldDown(time, itemToDestroy));
        FindObjectOfType<AudioManager>().Play("Shield");
    }

    private IEnumerator ShieldDown(float time, GameObject itemToDestroy)
    {
        yield return new WaitForSeconds(time);
        shieldUp = false;
        shieldObject.Stop();
        Destroy(itemToDestroy);
        GetComponent<ItemController>().currentEffectItem = null;
    }

    public void SetInfiniteShoot(float time, GameObject itemToDestroy)
    {
        infiniteShoot = true;
        infiniteShootParticleSystem.Play();
        itemRecharge.PutOnCooldown(time);
        StartCoroutine(InfiniteShootDown(time, itemToDestroy));
    }

    private IEnumerator InfiniteShootDown(float time, GameObject itemToDestroy)
    {
        yield return new WaitForSeconds(time);
        infiniteShoot = false;
        infiniteShootParticleSystem.Stop();
        Destroy(itemToDestroy);
        GetComponent<ItemController>().currentEffectItem = null;
    }

    public void ReceiveBitchSlap()
    {
        //if no protection -> stunned
        if (!shieldUp)
        {
            StunPlayer(GlobalVariables.GlobalVariablesInstance.SLAP_STUN_DURATION);
        }
    }

    public void StunPlayer(float duration)
    {
        isStunned = true;
        stunnedtimer = Time.time + duration;

        StartCoroutine(WaitToStartStunParticleSystem());
        anim.SetBool("Stunned", true);
        FindObjectOfType<AudioManager>().Play("Slapped");
    }

    IEnumerator WaitToStartStunParticleSystem()
    {
        yield return new WaitForSeconds(0.1f);
        stunParticleSystem.Play();
        slamParticleSystem.Play();
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class Z_Movement : MonoBehaviour
{
    //Game Element Variables
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Transform trans;
    private SpriteRenderer sp;
    private Animator anim;

    //Instance
    public static Z_Movement Instance { get; private set; }

    //Movement Variables
    private Vector2 playerInput { get; set; }
    [SerializeField] private float moveSpeed;
    [HideInInspector] public Vector3 zPosition;
    private Vector2 zDirection;
    [HideInInspector] private bool isMoving;
    [HideInInspector] public float zTimeAlive = 0f;

    //Mouse Variables
    private Vector3 mousePos;
    private Vector2 pointerInput;

    //Gun Variables
    private Gun_Parent gunParent;
    [SerializeField] private GameObject gun;
    [SerializeField] private Camera_Target camTar;
    [SerializeField] public InputActionReference movement, shoot, pointerPos, strike, interact, cover;

    //Shovel Variables
    [SerializeField] private LayerMask zombieLayers;
    private Collider2D[] hitZombies;
    [SerializeField] private float shovelCooldownTime = 5f;
    [HideInInspector] public float currentShovelCooldownTime;
    private bool canStrike = true;
    [HideInInspector] public bool isCovering;
    private Vector2 attackDirection;

    //Health Variables
    [SerializeField] private int maxHealth = 100;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public bool isDead = false;

    //Scene Variables
    private float deathTimer = 4f;
    private float currentDeathTimer;
    [HideInInspector] public bool deathCanvasStatus = false;
    [HideInInspector] public int currentZombieKillcount = 0;
    private int graveyardGrade;
    [SerializeField] private ScoreTracker scoretracker;

    //Sound Variables
    [SerializeField] private AudioSource run;
    [SerializeField] private AudioSource hurt;
    [SerializeField] private AudioSource death;
    [SerializeField] private AudioSource dig;
    [SerializeField] private AudioSource shovel;
    [SerializeField] private AudioSource health;
    [SerializeField] private AudioSource ammo;



    private void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gunParent = GetComponentInChildren<Gun_Parent>();

        zPosition = transform.position;
        currentHealth = maxHealth;

        currentShovelCooldownTime = 5f;
        currentDeathTimer = deathTimer;
    }

    private void Update()
    {
        pointerInput = GetPointerPosition();
        zPosition = transform.position;
        gunParent.pointerPos = pointerInput;
        camTar.camMousePos = new Vector3 (pointerInput.x, pointerInput.y, mousePos.z);
        Vector2 zDirection = (pointerInput - (Vector2)transform.position).normalized;
        zTimeAlive += Time.deltaTime;
        if (cover.action.IsInProgress())
        {
            isCovering = true;
        }
        else if(!cover.action.IsInProgress())
        {
            isCovering = false;
        }

        if (isDead == false)
        {
            playerInput = movement.action.ReadValue<Vector2>();

            if (zDirection.x < 0f)
            {
                sp.flipX = true;
            }
            else if (zDirection.x > 0f)
            {
                sp.flipX = false;
            }
            if (isCovering == true)
            {
                anim.SetBool("IsShoveling", true);
                SpriteRenderer gunEnabled = gun.GetComponent<SpriteRenderer>();
                gunEnabled.enabled = false;
                playerInput = Vector2.zero;
                if (!dig.isPlaying)
                {
                    dig.Play();
                }
            }
            else
            {
                anim.SetBool("IsShoveling", false);
                SpriteRenderer gunEnabled = gun.GetComponent<SpriteRenderer>();
                gunEnabled.enabled = true;
                dig.Stop();
            }
        }
        else
        {
            if (currentDeathTimer >= 0)
            {
                currentDeathTimer -= Time.deltaTime;
            }
            else
            {
                deathCanvasStatus = true;
            }
        }

        if (currentShovelCooldownTime >= 0)
        {
            currentShovelCooldownTime -= Time.deltaTime;
            canStrike = false;
        }
        else
        {
            canStrike = true;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playerInput * moveSpeed;
        if (playerInput.x == 0f && playerInput.y == 0f)
        {
            anim.SetBool("IsWalking", false);
            isMoving = false;
        }
        else
        {
            anim.SetBool("IsWalking", true);
            isMoving = true;
        }
    //Running Sound Player
        if (isMoving)
        {
            if (!run.isPlaying)
            {
                run.Play();
            }
        }
        else
        {
            run.Stop();
        }
    }

    //Get mouse position
    private Vector2 GetPointerPosition()
    {
        mousePos = pointerPos.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    {
        shoot.action.performed += PerformShoot;
        strike.action.performed += PerformStrike;
    }

    private void OnDisable()
    {
        shoot.action.performed -= PerformShoot;
        strike.action.performed -= PerformStrike;
    }
    private void PerformShoot(InputAction.CallbackContext obj)
    {
        if(gunParent == null)
        {
            Debug.Log("Gun_Parent is null");
            return;
        }
        else
        {
            gunParent.Shoot();
        }
    }

    private void PerformStrike(InputAction.CallbackContext obj)
    {
        if (gameObject == null)
        {
            Debug.Log("Z is null");
            return;
        }
        else
        {
            if(canStrike == true)
            {
                anim.SetTrigger("Strike");
                shovel.Play();
                if (sp.flipX == false)
                {
                    hitZombies = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 1.5f, transform.position.y), new Vector2(3, 3), 0f, zombieLayers);
                    attackDirection = Vector2.right;
                }
                else if (sp.flipX == true)
                {
                    hitZombies = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - 1.5f, transform.position.y), new Vector2(3, 3), 0f, zombieLayers);
                    attackDirection = Vector2.left;
                }
                foreach (Collider2D zombie in hitZombies)
                {
                    zombie.GetComponent<Zombie_Enemy>().ShovelTake(attackDirection);
                }
                currentShovelCooldownTime = shovelCooldownTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            hurt.Play();
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        death.Play();
        coll.enabled = false;
        isDead = true;
        anim.SetBool("IsDead", true);
        gunParent.gameObject.SetActive(false);
        gunParent.ammoCount = 0;
        playerInput = new Vector2(0, 0);

        if(currentZombieKillcount < 10)
        {
            graveyardGrade = 4;
        }
        else if (currentZombieKillcount > 10 && currentZombieKillcount < 25)
        {
            graveyardGrade = 3;
        }
        else if (currentZombieKillcount > 25 && currentZombieKillcount < 50)
        {
            graveyardGrade = 2;
        }
        else
        {
            graveyardGrade = 1;
        }

        scoretracker.graveGrade = graveyardGrade;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            ammo.Play();
            gunParent.ammoCount = 8;
        }
        if (collision.gameObject.CompareTag("Health"))
        {
            health.Play();
            if (currentHealth <= 80)
            {
                currentHealth += 20;
            }
        }
    }
}
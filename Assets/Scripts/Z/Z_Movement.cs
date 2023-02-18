using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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

    //Mouse Variables
    private Vector3 mousePos;
    private Vector2 pointerInput;

    //Gun Variables
    private Gun_Parent gunParent;
    [SerializeField] private Camera_Target camTar;
    [SerializeField] private InputActionReference movement, shoot, pointerPos;

    //Health Variables
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [HideInInspector] public bool isDead = false;

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
    }

    private void Update()
    {
        pointerInput = GetPointerPosition();
        zPosition = transform.position;
        gunParent.pointerPos = pointerInput;
        camTar.camMousePos = new Vector3 (pointerInput.x, pointerInput.y, mousePos.z);
        Vector2 zDirection = (pointerInput - (Vector2)transform.position).normalized;

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
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playerInput * moveSpeed;
        if (playerInput.x == 0f && playerInput.y == 0f)
        {
            anim.SetBool("IsWalking", false);
        }
        else
        {
            anim.SetBool("IsWalking", true);
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
    }

    private void OnDisable()
    {
        shoot.action.performed -= PerformShoot;
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        coll.enabled = false;
        isDead = true;
        anim.SetBool("IsDead", true);
        gunParent.gameObject.SetActive(false);
        gunParent.ammoCount = 0;
        playerInput = new Vector2(0, 0);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            gunParent.ammoCount = 8;
        }
        if (collision.gameObject.CompareTag("Health"))
        {
            currentHealth += 33;
            Debug.Log(currentHealth);
        }
    }
}
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

    //Movement Variables
    private Vector2 playerInput { get; set; }
    [SerializeField] private float moveSpeed;
    public Vector3 zPosition;

    //Mouse Variables
    private Vector3 mousePos;
    private Vector2 pointerInput;

    private Gun_Parent gunParent;
    [SerializeField] private Camera_Target camTar;

    [SerializeField] private InputActionReference movement, shoot, pointerPos;

    //Health Variables
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
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
        zPosition = transform.position;
        gunParent.pointerPos = pointerInput;
        camTar.mousePos = new Vector3 (pointerInput.x, pointerInput.y, mousePos.z);
        playerInput = movement.action.ReadValue<Vector2>();

        Vector2 zDirection = (pointerInput - (Vector2)transform.position).normalized;
        if (zDirection.x < 0f)
        {
            sp.flipX = true;
        }
        else if (zDirection.x > 0f)
        {
            sp.flipX = false;
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

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Died");
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
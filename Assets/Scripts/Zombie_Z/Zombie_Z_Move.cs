using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Zombie_Z_Move : MonoBehaviour
{
    //Game Element Variables
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Transform trans;
    private SpriteRenderer sp;
    private Animator anim;

    //Instance
    public static Zombie_Z_Move Instance { get; private set; }

    //Movement Variables
    private Vector2 playerInput { get; set; }
    [SerializeField] private float moveSpeed;
    [HideInInspector] public Vector3 zombie_Z_Position;

    //Mouse Variables
    private Vector3 mousePos;
    private Vector2 pointerInput;

    //Gun Variables
    [SerializeField] private Camera_Target camTar;
    [SerializeField] private InputActionReference movement, attack, pointerPos;

    //Health Variables
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [HideInInspector] public bool zombieZIsDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        zombie_Z_Position = transform.position;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        pointerInput = GetPointerPosition();
        zombie_Z_Position = transform.position;
        camTar.camMousePos = new Vector3(pointerInput.x, pointerInput.y, mousePos.z);
        Vector2 zDirection = (pointerInput - (Vector2)transform.position).normalized;

        if (zombieZIsDead == false)
        {
            playerInput = movement.action.ReadValue<Vector2>();

            if (playerInput.x < 0f)
            {
                anim.SetFloat("Horizontal", -1f);
                anim.SetFloat("Vertical", 0f);
                sp.flipX = true;
            }
            else if (playerInput.x > 0f)
            {
                anim.SetFloat("Horizontal", 1f);
                anim.SetFloat("Vertical", 0f);
                sp.flipX = false;
            }
            else if(playerInput.y > 0f)
            {
                sp.flipX = false;
                anim.SetFloat("Vertical", 1f);
                anim.SetFloat("Horizontal", 0f);
            }
            else if (playerInput.y < 0f)
            {
                sp.flipX = false;
                anim.SetFloat("Vertical", -1f);
                anim.SetFloat("Horizontal", 0f);
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
        attack.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        //Perform Attack
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
        coll.enabled = false;
        zombieZIsDead = true;
        anim.SetBool("IsDead", true);
        playerInput = new Vector2(0, 0);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}

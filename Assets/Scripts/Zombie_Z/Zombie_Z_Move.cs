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
    private AudioSource aud;

    //Instance
    public static Zombie_Z_Move Instance { get; private set; }

    //Movement Variables
    private Vector2 playerInput { get; set; }
    [SerializeField] private float moveSpeed;
    [HideInInspector] public Vector3 zombie_Z_Position;

    //Mouse Variables
    private Vector3 mousePos;
    private Vector2 pointerInput;

    //Attack Variables
    [SerializeField] private Camera_Target camTar;
    [SerializeField] private InputActionReference movement, attack, pointerPos;
    [SerializeField] private LayerMask npcLayers;
    private Collider2D[] hitNPC;
    private int attackDamage = 100;

    //Health Variables
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [HideInInspector] public bool zombieZIsDead = false;

    //Audio Variables
    
    //[SerializeField] private AudioClip ;


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

    public void Update()
    {
        pointerInput = GetPointerPosition();
        zombie_Z_Position = GetPosition();
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
       
        if (playerInput.x < 0f)
        {
            anim.SetFloat("Horizontal", -1f);
            anim.SetFloat("Vertical", 0f);
            anim.SetTrigger("Attack");
            hitNPC = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - 1.5f, transform.position.y), new Vector2(2, 3), 0f, npcLayers);
            sp.flipX = true;
        }
        else if (playerInput.x > 0f)
        {
            anim.SetFloat("Horizontal", 1f);
            anim.SetFloat("Vertical", 0f);
            anim.SetTrigger("Attack");
            hitNPC = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 1.5f, transform.position.y), new Vector2(2, 3), 0f, npcLayers);
            sp.flipX = false;
        }
        else if (playerInput.y > 0f)
        {
            sp.flipX = false;
            anim.SetFloat("Vertical", 1f);
            anim.SetFloat("Horizontal", 0f);
            anim.SetTrigger("Attack");
            hitNPC = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + 1.5f), new Vector2(2, 3), 0f, npcLayers);
        }
        else if (playerInput.y < 0f)
        {
            sp.flipX = false;
            anim.SetFloat("Vertical", -1f);
            anim.SetFloat("Horizontal", 0f);
            anim.SetTrigger("Attack");
            hitNPC = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y - 1.5f), new Vector2(2, 3), 0f, npcLayers);
        }
        foreach (Collider2D npc in hitNPC)
        {
            if (npc.gameObject.CompareTag("NPC"))
            {
                npc.GetComponent<NPC_Controller>().NPCTakeDamage(attackDamage);
            }
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

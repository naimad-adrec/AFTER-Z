using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using System;
using Unity.VisualScripting;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Zombie_Enemy : MonoBehaviour
{
    //Game Components
    [SerializeField] private AIPath aiPath;
    [SerializeField] private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private AudioSource aud;

    //Instance
    public static Zombie_Enemy Instance { get; private set; }

    //BoxCast Variables
    [SerializeField] private LayerMask zLayers;
    private Collider2D[] hitZ;
    private Vector2 boxSize = new Vector2(2.5f, 2.5f);

    //Health Variables
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;
    [SerializeField] private GameObject ammo;
    private int chance;

    //Attack Variables
    private int attackRange = 1;
    private int attackDamage = 20;
    private float nextAttackTime;
    [SerializeField] private float attackCooldown = 2;
    private bool canAttack;

    //Audio Variables
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip die;

    //Bullet Variables
    [SerializeField] private Bullet_Controller bullet;

    //Z Variables
    private GameObject z;
    private bool zIsDead;
    private Vector3 zNewPosition;

    IAstarAI ai;

    void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
        // Update the destination right before searching for a path as well.
        // This is enough in theory, but this script will also update the destination every
        // frame as the destination is used for debugging and may be used for other things by other
        // scripts as well. So it makes sense that it is up to date every frame.
        if (ai != null) ai.onSearchPath += Update;
    }

    void OnDisable()
    {
        if (ai != null) ai.onSearchPath -= Update;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        aud = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        canAttack = true;
    }

    private void Update()
    {
        zNewPosition = Z_Movement.Instance.zPosition;
        zIsDead = Z_Movement.Instance.isDead;

        if (rb != null && ai != null) ai.destination = zNewPosition;

        if (Vector3.Distance(gameObject.transform.position, zNewPosition) < attackRange)
        {
            if(canAttack == true && zIsDead == false)
            {
                //Attack player
                aud.clip = attackSound;
                aud.Play();
                aiPath.canMove = false;
                anim.SetTrigger("ZombieAttack");
                hitZ = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), boxSize, 0f, zLayers);
                StartCoroutine(AttackCooldown());
                foreach (Collider2D player in hitZ)
                {
                    player.GetComponent<Z_Movement>().TakeDamage(attackDamage);
                }
            }
            else if (canAttack == true && zIsDead == true)
            {
                coll.enabled = true;
                anim.SetBool("IsEating", true);
            }
            else if (canAttack == false && isDead == false)
            {
                if (hitZ.Length > 0)
                {
                    Array.Clear(hitZ, 0, hitZ.Length);
                }
            }
        }
        else
        {
            if(isDead == true)
            {
                aiPath.canMove = false;
            }
            else
            {
                aiPath.canMove = true;
            }

            if (canAttack == true && zIsDead == true)
            {
                coll.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(KnockBackWait());
        }
    }

    //Take damage and die functions
    private void ZombieTakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("ZombieHurt");
        chance = Random.Range(0, 7);
        Debug.Log(chance);

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Z_Movement.Instance.currentZombieKillcount += 1;
        StartCoroutine(WaitForDeathAnim());
        if (chance == 1)
        {
            Instantiate(ammo, transform.position, transform.rotation);
        }
    }


    //Timed functions
    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        aiPath.canMove = true;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator WaitForDeathAnim()
    {
        aud.clip = die;
        aud.Play();
        anim.SetBool("IsDead", true);
        isDead = true;
        coll.enabled = false;
        canAttack = false;
        aiPath.canMove = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private IEnumerator KnockBackWait()
    {
        ZombieTakeDamage(bullet.bulletDamage);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = new Vector2(0f, 0f);
    }
}
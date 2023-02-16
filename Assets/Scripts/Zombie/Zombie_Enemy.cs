using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using System;

public class Zombie_Enemy : MonoBehaviour
{
    //Game Components
    [SerializeField] private AIPath aiPath;
    [SerializeField] private Z_Movement z;
    [SerializeField] private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;

    //BoxCast Variables
    [SerializeField] private LayerMask zLayers;
    private Collider2D[] hitZ;
    private Vector2 boxSize = new Vector2(2.5f, 2.5f);

    //Health Variables
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    //Attack Variables
    private int attackRange = 1;
    private int attackDamage = 20;
    private float nextAttackTime;
    [SerializeField] private float attackCooldown = 2;
    private bool canAttack;

    //Bullet Variables
    [SerializeField] private Bullet_Controller bullet;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
        canAttack = true;
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, z.zPosition) < attackRange)
        {
            if(canAttack == true)
            {
                //Attack player
                aiPath.canMove = false;
                hitZ = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), boxSize, 0f, zLayers);
                StartCoroutine(AttackCooldown());
                foreach (Collider2D player in hitZ)
                {
                    player.GetComponent<Z_Movement>().TakeDamage(attackDamage);
                }
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(WaitForDeathAnim());
    }


    //Timed functions
    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        aiPath.canMove = true;
        Debug.Log("Player hit");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator WaitForDeathAnim()
    {
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
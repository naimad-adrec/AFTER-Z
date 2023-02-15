using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using System;

public class Zombie_Enemy : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    private BoxCollider2D coll;
    private Animator anim;

    [SerializeField] private LayerMask zLayers;
    private Collider2D[] hitZ;

    private Vector2 boxSize = new Vector2(2.5f, 2.5f);

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private int attackDamage = 20;
    private float nextAttackTime;
    [SerializeField] private float attackCooldown = 2;
    private bool canAttack;

    [SerializeField] private Bullet_Controller bullet;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        canAttack = true;
    }

    private void Update()
    {
        if (aiPath.reachedEndOfPath == true)
        {
            if(canAttack == true)
            {
                hitZ = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), boxSize, 0f, zLayers);
                StartCoroutine(AttackCooldown());
                foreach (Collider2D player in hitZ)
                {
                    player.GetComponent<Z_Movement>().TakeDamage(attackDamage);
                }
            }
            else
            {
                if(hitZ.Length > 0)
                {
                    Array.Clear(hitZ, 0, hitZ.Length);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Zombie Hit");
            ZombieTakeDamage(bullet.bulletDamage);
        }
    }

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

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        Debug.Log("Player hit");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator WaitForDeathAnim()
    {
        anim.SetBool("IsDead", true);
        canAttack = false;
        aiPath.canMove = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
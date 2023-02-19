using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    //Game Components
    [SerializeField] private AIPath aiPath;
    private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private AudioSource aud;

    //Health Variables
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        aud = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void NPCTakeDamage(int damage)
    {
        currentHealth -= damage;
        //anim.SetTrigger("NPCHurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(WaitForDeathAnim());
    }

    private IEnumerator WaitForDeathAnim()
    {
        Debug.Log("I died");
        //anim.SetBool("IsDead", true);
        isDead = true;
        coll.enabled = false;
        aiPath.canMove = false;
        yield return new WaitForSeconds(5);
    }

}

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

    //Ai Variables
    private float runDistance = 10f;
    private float wanderDistanceX;
    private float wanderDistanceY;
    private Vector3 newZZomPosition;
    private Vector3 distance;
    private float idleTimer = 5f;
    private float currentIdleTimer;
    public bool isScared;

    //Zombie Variable
    [SerializeField] private GameObject zombie;

    private IAstarAI ai;

    private void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
        // Update the destination right before searching for a path as well.
        // This is enough in theory, but this script will also update the destination every
        // frame as the destination is used for debugging and may be used for other things by other
        // scripts as well. So it makes sense that it is up to date every frame.
        if (ai != null) ai.onSearchPath += Update;
    }

    private void OnDisable()
    {
        if (ai != null) ai.onSearchPath -= Update;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        aud = GetComponent<AudioSource>();
        currentHealth = maxHealth;

        newZZomPosition = GameObject.Find("Zombie Z").GetComponent<Zombie_Z_Move>().zombie_Z_Position;
    }

    private void Update()
    {
        newZZomPosition = GameObject.Find("Zombie Z").GetComponent<Zombie_Z_Move>().zombie_Z_Position;
        distance = transform.position - newZZomPosition;
        if (distance.x < runDistance && distance.y < runDistance)
        {
            ai.maxSpeed = 10;
            ai.destination = distance;
            isScared = true;
        }
        else
        {
            isScared = false;
            if (currentIdleTimer >= 0)
            {
                currentIdleTimer -= Time.deltaTime;
            }
            else
            {
                ai.maxSpeed = 5f;
                wanderDistanceX = Random.Range(-5, 5);
                wanderDistanceY = Random.Range(-5, 5);
                ai.destination = new Vector3((transform.position.x + wanderDistanceX), (transform.position.y + wanderDistanceY), transform.position.z);
                currentIdleTimer = idleTimer;
            }
        }
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
        Instantiate(zombie, transform.position, transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
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
    private float runDistance = 5f;
    private float wanderDistanceX;
    private float wanderDistanceY;
    private Vector3 newZZomPosition;
    private float distance;
    private float xDistance;
    private float yDistance;
    private float idleTimer = 5f;
    private float currentIdleTimer;
    public bool isScared;

    //Zombie Variable
    [SerializeField] private GameObject zombie;

    private IAstarAI ai;

    private void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
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

        newZZomPosition = Zombie_Z_Move.Instance.zombie_Z_Position;
    }

    private void Update()
    {
        newZZomPosition = Zombie_Z_Move.Instance.zombie_Z_Position;
        xDistance = newZZomPosition.x - transform.position.x;
        yDistance = newZZomPosition.y - transform.position.y;


        distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
        if (distance < 5)
        {
            ai.maxSpeed = 10;
            if (newZZomPosition.x > transform.position.x)
            {
                ai.destination = new Vector3((transform.position.x - distance), (transform.position.y - distance), transform.position.z);
            }
            else
            {
                ai.destination = new Vector3((transform.position.x + distance), (transform.position.y + distance), transform.position.z);
            }
            isScared = true;
        }
        else if (distance > 5)
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
        //Zombie_Z_Move.Instance.totalNPCKillCount += 1;
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
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class NPC_Zombie : MonoBehaviour
{
    //Game Components
    [SerializeField] private AIPath aiPath;
    [SerializeField] private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private AudioSource aud;

    //Instance
    public static NPC_Zombie Instance { get; private set; }

    //BoxCast Variables
    [SerializeField] private LayerMask zLayers;
    private Collider2D[] hitZ;
    private Vector2 boxSize = new Vector2(2.5f, 2.5f);

    //Attack Variables
    //private int attackRange = 1;
    //private int attackDamage = 100;
    //private float nextAttackTime;
    //[SerializeField] private float attackCooldown = 2;
    //private bool canAttack;

    //Audio Variables
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip die;

    //Z Variables
    private Vector3 newZZomPosition;
    private Vector3 aroundZZom;

    //AI Variables
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

        newZZomPosition = Zombie_Z_Move.Instance.zombie_Z_Position;
        //canAttack = true;
    }

    private void Update()
    {
        newZZomPosition = Zombie_Z_Move.Instance.zombie_Z_Position;
        aroundZZom = new Vector3(newZZomPosition.x, newZZomPosition.y, transform.position.z);

        if (transform.position.x > newZZomPosition.x)
        {
            aroundZZom.x = aroundZZom.x + 1;
        }
        else if(transform.position.x < newZZomPosition.x)
        {
            aroundZZom.x = aroundZZom.x - 1;
        }

        if (transform.position.y > newZZomPosition.y)
        {
            aroundZZom.y = aroundZZom.y + 1;
        }
        else if (transform.position.y < newZZomPosition.y)
        {
            aroundZZom.y = aroundZZom.y - 1;
        }

        if (rb != null && ai != null) ai.destination = aroundZZom;
    }

    //Timed functions
    //private IEnumerator AttackCooldown()
    //{
        //canAttack = false;
        //aiPath.canMove = true;
        //yield return new WaitForSeconds(attackCooldown);
        //canAttack = true;
    //}
}

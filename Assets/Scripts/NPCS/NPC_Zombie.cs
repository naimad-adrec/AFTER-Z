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
    private GameObject zZom;
    private Vector3 newZZomPosition;

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

        newZZomPosition = GameObject.Find("Zombie Z").GetComponent<Zombie_Z_Move>().zombie_Z_Position;
        //canAttack = true;
    }

    private void Update()
    {
        newZZomPosition = GameObject.Find("Zombie Z").GetComponent<Zombie_Z_Move>().zombie_Z_Position;

        if (rb != null && ai != null) ai.destination = newZZomPosition;
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

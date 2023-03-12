using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Grave_Controller : MonoBehaviour
{
    //Game Components
    private BoxCollider2D coll;
    [HideInInspector] public Animator anim;

    //Instance
    public static Grave_Controller Instance { get; private set; }

    //Zombie Variable
    [SerializeField] private GameObject zombie;
    private Vector3 zombieSpawnPoint;
    private int zombieSpawnCount = 0;

    //Zombie Spawn Timers
    private float zombieSpawnTime;
    private float currentZombieSpawnTime;

    //Unearthed Timers
    [SerializeField] private GameObject health;
    private float graveUnearthTime;
    private float currentGraveUnearthTime;
    private float currentCoverTime = 0;
    private Vector3 graveAmmoSpawnPoint;

    private void Start()
    {
        Instance = this;
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        coll.enabled = false;

        currentZombieSpawnTime = 9.5f;

        graveUnearthTime = Random.Range(10f, 50f);
        currentGraveUnearthTime = graveUnearthTime;

        zombieSpawnPoint = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        graveAmmoSpawnPoint = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
    }

    private void Update()
    {
        zombieSpawnCount = transform.childCount;
        if(currentGraveUnearthTime >= 0)
        {
            coll.enabled = false;
            currentGraveUnearthTime -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("IsUnearthed", true);
            if (currentZombieSpawnTime >= 0)
            {
                currentZombieSpawnTime -= Time.deltaTime;
            }
            else
            {
                coll.enabled = true;
                SpawnZombie();
                currentZombieSpawnTime = Random.Range(15f, 30f);
            }
        }
    }

    private void SpawnZombie()
    {
        if (zombieSpawnCount <= 2)
        {
            Instantiate(zombie, zombieSpawnPoint, transform.rotation, gameObject.transform);
        }
    }

    public void CoverGrave()
    {
        currentCoverTime += Time.deltaTime;
        if(currentCoverTime >= 2.5f)
        {
            currentGraveUnearthTime = graveUnearthTime;
            currentZombieSpawnTime = zombieSpawnTime;
            anim.SetBool("IsUnearthed", false);
            Instantiate(health, graveAmmoSpawnPoint, transform.rotation);
            currentCoverTime = 0;
        }
    }
}

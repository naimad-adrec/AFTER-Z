using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave_Controller : MonoBehaviour
{
    //Game Components
    private BoxCollider2D coll;
    private Animator anim;

    //Zombie Variable
    [SerializeField] private GameObject zombie;
    private Vector3 zombieSpawnPoint;
    private int zombieSpawnCount = 0;

    //Zombie Spawn Timers
    private float zombieSpawnTime;
    private float currentZombieSpawnTime;

    //Unearthed Timers
    private float graveUnearthTime;
    private float currentGraveUnearthTime;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        coll.enabled = false;

        currentZombieSpawnTime = 9.5f;

        graveUnearthTime = Random.Range(10f, 50f);
        currentGraveUnearthTime = graveUnearthTime;

        zombieSpawnPoint = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
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
}

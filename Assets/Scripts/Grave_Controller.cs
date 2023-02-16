using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave_Controller : MonoBehaviour
{
    private BoxCollider2D coll;
    private Animator anim;

    [SerializeField] private GameObject zombie;

    private bool unearthed;
    private Vector3 zombieSpawnPoint;

    [SerializeField] private float zombieSpawnTime;
    private float currentZombieSpawnTime;

    [SerializeField] private float graveUnearthTime;
    private float currentGraveUnearthTime;

    

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        coll.enabled = false;

        currentZombieSpawnTime = zombieSpawnTime;

        unearthed = false;
        float unearthTimer = Random.Range(10f, 20f);

        zombieSpawnPoint = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
    }

    private void Update()
    {
        if(currentGraveUnearthTime >= 0)
        {
            currentGraveUnearthTime -= Time.deltaTime;
        }
        else
        {
            coll.enabled = true;
            if (currentZombieSpawnTime >= 0)
            {
                currentZombieSpawnTime -= Time.deltaTime;
            }
            else
            {
                SpawnZombie();
                currentZombieSpawnTime = zombieSpawnTime;
            }
        }
    }

    private void SpawnZombie()
    {
        Instantiate(zombie, zombieSpawnPoint, transform.rotation);
    }
}

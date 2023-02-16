using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave_Controller : MonoBehaviour
{
    private BoxCollider2D coll;
    private Animator anim;

    [SerializeField] private GameObject zombie;

    private bool unearthed;
    private float currentTime = 0f;
    private float unearthTimer;
    private Vector3 zombieSpawnPoint;
    private bool zombieSpawnTime;
    

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        coll.enabled = false;
        unearthed = false;
        zombieSpawnTime = false;
        float unearthTimer = Random.Range(10f, 20f);
        currentTime = unearthTimer;
        zombieSpawnPoint = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
}

    private void Update()
    {
        if (unearthed == false)
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime >= unearthTimer)
            {
                StartCoroutine(Unearth());
            }
        }
        else if(unearthed == true && zombieSpawnTime == true)
        {
            SpawnZombie();
        }
    }

    private IEnumerator Unearth()
    {
        anim.SetBool("IsUnearthed", true);
        yield return new WaitForSeconds(2);
        unearthed = true;
        zombieSpawnTime = true;
    }

    private void SpawnZombie()
    {
        //Instantiate(zombie, zombieSpawnPoint, transform.rotation);
        zombieSpawnTime = false;
    }
}

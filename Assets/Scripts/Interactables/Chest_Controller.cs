using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_Controller : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject health;

    private Vector3 chestSpawnPoint;

    private void Start()
    {
        anim = GetComponent<Animator>();

        chestSpawnPoint = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
    }

    public void OpenChest()
    {
        anim.SetBool("IsOpened", true);
        Instantiate(health, chestSpawnPoint, transform.rotation, gameObject.transform);
    }
}

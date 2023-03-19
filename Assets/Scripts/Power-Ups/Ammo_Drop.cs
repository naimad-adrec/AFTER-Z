using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Drop : MonoBehaviour
{

    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            aud.Play();
            Destroy(gameObject);
        }
    }
}

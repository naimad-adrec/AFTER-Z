using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Drop : MonoBehaviour
{
    private AudioSource healthAudio;

    private void Start()
    {
        healthAudio = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthAudio.Play();
            Destroy(gameObject);
        }
    }
}

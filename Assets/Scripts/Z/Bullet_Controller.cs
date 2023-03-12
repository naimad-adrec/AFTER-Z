using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    public int bulletDamage = 33;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Grave") || !collision.gameObject.CompareTag("Interactible"))
        {
            Destroy(gameObject);
        }
    }
}
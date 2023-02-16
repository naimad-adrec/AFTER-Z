using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Parent : MonoBehaviour
{
    public Vector2 pointerPos { get; set; }
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [SerializeField] private float bulletForce = 20f;

    private void Update()
    {
        Vector2 direction = (pointerPos - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if(direction.x < 0f)
        {
            scale.y = -1;
        }
        else if(direction.x > 0f)
        {
            scale.y = 1;
        }
        transform.localScale = scale;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firingPoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
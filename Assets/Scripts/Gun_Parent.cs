using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Parent : MonoBehaviour
{
    public Vector2 pointerPos { get; set; }

    private void Update()
    {
        Vector2 direction = (pointerPos - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if(direction.x < 0f)
        {

        }
    }
}

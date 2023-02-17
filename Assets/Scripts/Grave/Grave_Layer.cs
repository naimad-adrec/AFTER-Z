using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave_Layer : MonoBehaviour
{
    private SpriteRenderer sp;
    private Vector3 zPos;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        zPos = Z_Movement.Instance.transform.position;

        if(zPos.y > transform.position.y)
        {
            sp.sortingLayerName = "Player";
            sp.sortingOrder = 1;
        }
        else
        {
            sp.sortingLayerName = "Ground";
            sp.sortingOrder = 1;
        }
    }
}

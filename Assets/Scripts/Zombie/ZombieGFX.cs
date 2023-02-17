using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieGFX : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    private Animator anim;
    private SpriteRenderer sp;

    private Vector3 zLayerPos;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        zLayerPos = Z_Movement.Instance.zPosition;

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        ZombieLayerSort(zLayerPos);
    }

    private void ZombieLayerSort(Vector3 playerPosition)
    {
        if (playerPosition.y > transform.position.y)
        {
            sp.sortingLayerName = "Player";
            sp.sortingOrder = 1;
        }
        else
        {
            sp.sortingLayerName = "Enemy";
            sp.sortingOrder = 0;
        }
    }
}

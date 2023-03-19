using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class NPC_GFX : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    [SerializeField] private NPC_Controller npcMove;
    private bool deathStatus = false;
    private Animator anim;
    private SpriteRenderer sp;

    private Vector3 zZomLayerPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        deathStatus = transform.parent.GetComponent<NPC_Controller>().isDead;
    }

    private void Update()
    {
        deathStatus = transform.parent.GetComponent<NPC_Controller>().isDead;
        zZomLayerPos = Zombie_Z_Move.Instance.zombie_Z_Position;

        if(deathStatus == false)
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            if (npcMove.isScared == true)
            {
                anim.SetBool("IsScared", true);
            }
            else if (npcMove.isScared != true)
            {
                anim.SetBool("IsScared", false);
            }

            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            anim.SetBool("IsDead", true);
        }

        ZombieLayerSort(zZomLayerPos);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    private Vector3 npcSpawnPoint;
    private int npcSpawnCount = 0;
    private float currentNPCSpawnTime = 1f;

    private void Update()
    {
        npcSpawnCount = transform.childCount;
        if (currentNPCSpawnTime >= 0)
        {
            currentNPCSpawnTime -= Time.deltaTime;
        }
        else
        {
            SpawnNPC();
            currentNPCSpawnTime = Random.Range(1f, 3f);
        }
    }

    private void SpawnNPC()
    {
        if (npcSpawnCount <= 10)
        {
            Instantiate(npc, transform.position, transform.rotation, gameObject.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_AI : MonoBehaviour
{
    private Vector3 startingPos;
    private Vector3 roamPos;
    private void Start()
    {
        startingPos = transform.position;
        roamPos = RoamingPosition();
    }

    private void Update()
    {
        
    }

    private Vector3 RoamingPosition()
    {
        return startingPos + RandomDirection() * Random.Range(10f, 70f);
    }

    private Vector3 RandomDirection()
    {
        return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0f).normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelUI : MonoBehaviour
{
    [SerializeField] private Slider cooldown;

    private float currentCooldown;

    private void Start()
    {
        currentCooldown = GameObject.Find("Z").GetComponent<Z_Movement>().currentShovelCooldownTime;
        cooldown.value = 5;
    }

    private void Update()
    {
        currentCooldown = GameObject.Find("Z").GetComponent<Z_Movement>().currentShovelCooldownTime;
        cooldown.value = currentCooldown;
    }
}

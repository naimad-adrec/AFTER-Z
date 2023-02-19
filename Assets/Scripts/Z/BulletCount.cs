using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletCount : MonoBehaviour
{
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Gun_Parent gun;

    private int count;

    private void Update()
    {
        count = gun.ammoCount;
        UpdateCount();
    }

    private void UpdateCount()
    {
        countText.text = count.ToString();
    }
}

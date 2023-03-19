using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Target : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform zTrans;
    [SerializeField] private float threshold;
    public Vector3 camMousePos;

    void Update()
    {
        Vector3 targetPos = (zTrans.position + camMousePos) / 2f;
        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + zTrans.position.x, threshold + zTrans.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + zTrans.position.y, threshold + zTrans.position.y);

        this.transform.position = targetPos;
    }

}

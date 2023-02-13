using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Z_Movement : MonoBehaviour
{
    //Game Element Variables
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Transform trans;

    //Movement Variables
    private Vector2 playerInput { get; set; }
    [SerializeField] private float moveSpeed;

    private Vector3 mousePos;
    private Vector2 pointerInput;

    private Gun_Parent gunParent;

    [SerializeField] private InputActionReference movement, shoot, pointerPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
        gunParent = GetComponentInChildren<Gun_Parent>();
    }

    private void Update()
    {
        pointerInput = GetPointerPosition();
        gunParent.pointerPos = pointerInput;
        playerInput = movement.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = playerInput * moveSpeed;
    }

    private Vector2 GetPointerPosition()
    {
        mousePos = pointerPos.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    {
        shoot.action.performed += PerformShoot;
    }

    private void OnDisable()
    {
        shoot.action.performed -= PerformShoot;
    }

    private void PerformShoot(InputAction.CallbackContext obj)
    {
        //if(Gun_Parent == null)
        //{
        //    Debug.Log("Gun_Parent is null");
        //}
        //else
        //{

        //}
        //throw new NotImplementedException();
    }
}


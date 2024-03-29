using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Z_Movement zInteract;

    private bool isInRange;
    [SerializeField] private UnityEvent interactAction;
    [SerializeField] private UnityEvent holdAction;

    private bool eIsPressed = false;
    public bool spaceIsHeld;

    private void Start()
    {
        eIsPressed = zInteract.interact.action.IsPressed();
    }

    private void Update()
    {
        eIsPressed = zInteract.interact.action.IsPressed();
        spaceIsHeld = Z_Movement.Instance.isCovering;
        if (isInRange == true)
        {
            if (eIsPressed)
            {
                interactAction.Invoke();
            }
            if (spaceIsHeld && GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Grave_Opened"))
            {
                holdAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
    }
}

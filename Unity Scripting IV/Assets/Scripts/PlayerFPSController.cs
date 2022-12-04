using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(MouseLook))]
public class PlayerFPSController : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private MouseLook mouseLook;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockstate = CursorLockMode.Locked;

        GameObject.Find("Capsule").gameObject.SetActive(false);

        characterMovement = GetComponent<CharacterMovement>();
        mouseLook = GetComponent<MouseLook>();
    }

    private void Update()
    {
        movement();
        rotation();
    }

    private void movement()
    {
        float hMovementInput = hMovementInput.GetAxisRaw("Horizontal");
        float vMovementInput = vMovementInput.GetAxisRaw("Vertical");

        bool jumpInput = Input.GetButtonDown("Jump");
        bool dashInput = Input.GetButton("Dash");

        characterMovement.moveCharacter(hMovementInput, vMovementInput, jumpInput, dashInput);
    }

    private void rotation()
    {
        float hRotationInput = Input.GetAxis("Mouse X");
        float vRotationInput = Input.GetAxis("Mouse Y");

        mouseLook.handleRotation(hRotationInput, vRotationInput);
    }
}

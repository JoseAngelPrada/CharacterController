using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement:MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f;
    [Range(0.01f, 1f)]
    public float forwardJumpFactor = 0.05f;
    public float Gravity = -9.8f;
    public float DashFactor = 2f;
    public Vector3 Drag = new Vector3(1f, 2f, 1f);
    public float smoothTime = 0.15f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 smoothMoveDirection;
    private Vector3 smoother;
    private Vector3 horizontalVelocity;

    public bool isGrounded { get { return characterController.isGrounded; } }
    public float currentSpeed { get { return horizontalVelocity.magnitude; } }
    public float currentNormalizedSpeed { get { return horizontalVelocity.normalized.magnitude; } }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void moveCharacter(float hInput, float vInput, bool jump, bool dash)
    {
        float deltaTime = Time.deltaTime;
        float dashF = 1f;

        if (characterController.isGrounded)
        {
            moveDirection = (hInput * transform.right + vInput * transform.forward).normalized;

            if (dash) dashF = DashFactor;
            if (jump)
            {
                if(Mathf.Abs(moveDirection.x) > 0f || Mathf.Abs(moveDirection.z) > 0f)
                {
                    moveDirection += moveDirection.normalized * (Mathf.Sqrt(jumpHeight * forwardJumpFactor * -Gravity / 2) * dashF);
                }

                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * Gravity);
            }
        }

        moveDirection.y += Gravity * deltaTime;

        moveDirection.x /= 1 + Drag.x * deltaTime;
        moveDirection.y /= 1 + Drag.y * deltaTime;
        moveDirection.z /= 1 + Drag.z * deltaTime;

        smoothMoveDirection = Vector3.SmoothDamp(smoothMoveDirection, moveDirection, ref smoother, smoothTime);

        smoothMoveDirection.y = moveDirection.y;

        characterController.Move(smoothMoveDirection * (deltaTime * speed * dashF));

        horizontalVelocity.Set(characterController.velocity.x, 0, characterController.velocity.x);
    }
}

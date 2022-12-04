using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public GameObject camerasParent;
    public float hRotationSpeed = 100f;
    public float vRotationSpeed = 80f;
    public float maxVerticalAngle;
    public float minVerticalAngle;
    public float smoothTime = 0.05f;

    float vCamRotationAngles;
    float hPlayerRotation;
    float currentHVelocity;
    float currentVVelocity;
    float targetCamEulers;
    Vector3 targetCamRotation;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void handleRotation(float hInput, float vInput)
    {
        float targetPlayerRotation = hInput * hRotationSpeed * Time.deltaTime;
        targetCamEulers += vInput * vRotationSpeed * Time.deltaTime;

        hPlayerRotation = Mathf.SmoothDamp(hPlayerRotation, targetPlayerRotation, ref currentHVelocity, smoothTime);
        transform.Rotate(0f, hPlayerRotation, 0f);

        targetCamEulers = Mathf.Clamp(targetCamEulers, minVerticalAngle, maxVerticalAngle);
        vCamRotationAngles = Mathf.SmoothDamp(vCamRotationAngles, targetCamEulers, ref currentVVelocity, smoothTime);
        targetCamRotation.Set(-vCamRotationAngles, 0f, 0f);
        camerasParent.transform.localEulerAngles = targetCamRotation;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float KeyMovementSpeed = 20.0f;
    public float ZoomSpeed = 500.0f;
    private Camera controllerdCamera;
    void Start()
    {
        controllerdCamera = gameObject.GetComponent<Camera>();
    }

    void Update()
    {

    }

    public void MoveCameraLeftRight(float v)
    {
        controllerdCamera.transform.Translate(v * KeyMovementSpeed, 0.0f, 0.0f, Space.World);

    }

    public void MoveCameraForwardBack(float v)
    {
        controllerdCamera.transform.Translate(0.0f, 0.0f, v * KeyMovementSpeed, Space.World);
    }

    public void ZoomCamera(float zoomInput)
    {
        controllerdCamera.transform.Translate(0, 0, zoomInput * ZoomSpeed, Space.Self);
    }
}
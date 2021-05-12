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

    public void MoveCamera(float horizontalInput, float verticalInput){
        controllerdCamera.transform.Translate(horizontalInput * KeyMovementSpeed, 0 ,verticalInput * KeyMovementSpeed, Space.World);
    }

    public void ZoomCamera(float zoomInput){
        controllerdCamera.transform.Translate(0, 0 , zoomInput * ZoomSpeed, Space.Self);
    }
}
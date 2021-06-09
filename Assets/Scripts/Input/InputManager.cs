using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private CameraController mainCameraController;

    private GameObject marker;
    void Start()
    {
        Instance = this;

        mainCameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        Bind<float>(Input.GetAxis("Horizontal") * Time.deltaTime,
            mainCameraController.MoveCameraLeftRight);
        Bind<float>(Input.GetAxis("Vertical") * Time.deltaTime,
            mainCameraController.MoveCameraForwardBack);

        Bind<float>(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime,
                mainCameraController.ZoomCamera);
                
        // Bind<bool>(Input.GetMouseButtonDown(0), interface.Select);
    }

    private void Bind<T>(T v, Action<T> action)
    {
        action(v);
    }

    public bool LeftClick()
    {
        return Input.GetMouseButtonDown(0);
    }

    private bool TryGetClickHit(out RaycastHit hit)
    {
        return (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            /*&& !EventSystem.current.IsPointerOverGameObject()*/;
    }

    public Maybe<SurfacePoint> GetSurfacePointUnderCursor()
    {
        RaycastHit hit;
        if (TryGetClickHit(out hit))
        {
            if (hit.collider.gameObject.tag == "Terrain")
            {
                BoardRepresentation br = hit.collider.gameObject
                    .GetComponent<BoardRepresentation>();

                return br.GetSurfacePoint(hit.triangleIndex, hit.point);
            }
        }

        return new Maybe<SurfacePoint>.Nothing();
    }
}
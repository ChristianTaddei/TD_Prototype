using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private CameraController mainCameraController;

    public Command ClickCommand;

    private GameObject marker;
    void Start()
    {
        Instance = this;

        mainCameraController = Camera.main.GetComponent<CameraController>();

        marker = (GameObject)Resources.Load("Prefabs/marker");
        marker.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    private SurfacePoint oldP = null;
    private List<GameObject> markers = new List<GameObject>();
    void Update()
    {
        mainCameraController.MoveCamera(
            Input.GetAxis("Horizontal") * Time.deltaTime,
            Input.GetAxis("Vertical") * Time.deltaTime);
        mainCameraController.ZoomCamera(
            Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime);

        if (LeftClick())
        {
            ClickCommand.Execute();
        }
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

    // public bool TryGetTokenUnderCursor(out IRepresentable hitToken)
    // {
    //     RaycastHit hit;
    //     if (TryGetClickHit(out hit))
    //     {
    //         IRepresentation hitRepresentation = hit.collider.gameObject
    //                 .GetComponentInParent<IRepresentation>();

    //         if (hitRepresentation != null )
    //         {
    //             hitToken = hitRepresentation.RepresentedObject;
    //             return true;
    //         } 
    //     }

    //     hitToken = default;
    //     return false;
    // }


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
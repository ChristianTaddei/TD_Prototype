using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private CameraController mainCameraController;

    void Start()
    {
        Instance = this;
        mainCameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        mainCameraController.MoveCamera(
            Input.GetAxis("Horizontal") * Time.deltaTime,
            Input.GetAxis("Vertical") * Time.deltaTime);
        mainCameraController.ZoomCamera(
            Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime);
    }

    public bool LeftClick()
    {
        return Input.GetMouseButtonDown(0);
    }

    private bool TryGetClickHit(out RaycastHit hit)
    {
        return (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            && !EventSystem.current.IsPointerOverGameObject();
    }

    public bool TryGetTokenUnderCursor(out IRepresentable hitToken)
    {
        RaycastHit hit;
        if (TryGetClickHit(out hit))
        {
            IRepresentation hitRepresentation = hit.collider.gameObject
                    .GetComponentInParent<IRepresentation>();

            if (hitRepresentation != null )
            {
                hitToken = hitRepresentation.RepresentedObject;
                return true;
            } 
        }

        hitToken = default;
        return false;
    }


    public bool TryGetBoardPositionUnderCursor(out SurfacePoint boardPosition)
    {
        RaycastHit hit;
        if (TryGetClickHit(out hit))
        {
            if (hit.collider.gameObject.tag == "Terrain")
            {
                BoardRepresentation tr = hit.collider.gameObject
                    .GetComponent<BoardRepresentation>();

                return tr.TryGetBoardPositionFromHit(hit, out boardPosition);
            }
        }

        boardPosition = default;
        return false;
    }
}
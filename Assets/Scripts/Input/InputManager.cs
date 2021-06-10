using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
	private CameraController mainCameraController;

	void Start()
	{
		mainCameraController = Camera.main.GetComponent<CameraController>();
	}

	void Update()
	{
		// Forward axis
		Execute<float>(Input.GetAxis("Horizontal") * Time.deltaTime,
		    mainCameraController.MoveCameraLeftRight);
		Execute<float>(Input.GetAxis("Vertical") * Time.deltaTime,
		    mainCameraController.MoveCameraForwardBack);

		Execute<float>(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime,
			mainCameraController.ZoomCamera);

		// Forward keys and buttons

		// Execute<bool>(Input.GetMouseButtonDown(0), forwardIfClickedRepresentation); // the alternative (clicked interface) is part of UI
	}

	// private void forwardIfClickedRepresentation(bool pressed)
	// {
	// 	RaycastHit hit;
	// 	if (pressed
	// 	    && !EventSystem.current.IsPointerOverGameObject()
	// 	    && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
	// 	{
	// 		if (hit.collider.gameObject.tag == "Terrain")
	// 		{
	// 			// BoardRepresentation br = hit.collider.gameObject
	// 			//     .GetComponent<BoardRepresentation>();

	// 			// Maybe<SurfacePoint> msp = br.GetSurfacePoint(hit.triangleIndex, hit.point);
    //             // if(msp.HasValue()){

    //             // }
	// 		}
	// 	}
	// }

	private void Execute<T>(T v, Action<T> action)
	{
		action(v);
	}
}
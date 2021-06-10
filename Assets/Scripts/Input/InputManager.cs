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

	public bool TryGetRaycastHit(out RaycastHit hit)
	{
		return (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		   /* && !EventSystem.current.IsPointerOverGameObject()*/;
	}


	private void Execute<T>(T v, Action<T> action)
	{
		action(v);
	}
}
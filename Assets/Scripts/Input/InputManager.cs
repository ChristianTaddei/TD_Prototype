using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
	private CameraController mainCameraController;

	private Dictionary<Func<bool>, Action> KeyBindings = new Dictionary<Func<bool>, Action>();

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

		// Forward bound keys and buttons
		foreach (KeyValuePair<Func<bool>, Action> entry in KeyBindings)
		{
			if (entry.Key.Invoke() == true)
			{
				entry.Value.Invoke();
			}
		}
	}

	public bool TryGetRaycastHit(out RaycastHit hit)
	{
		return (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit));
	}

	public void Bind(object mouse0, Action action)
	{
		KeyBindings.Add(() => Input.GetMouseButtonDown(0) /*&& !EventSystem.current.IsPointerOverGameObject()*/, action);
	}

	private void Execute<T>(T v, Action<T> action)
	{
		action(v);
	}
}
using UnityEngine;

public class RaycastMediator
{
	// TODO: not really mediating atm, will do with more repres?
	
	public InputManager InputManager { get; }

	public RaycastMediator(InputManager inputManager)
	{
		InputManager = inputManager;
	}

	// TODO: should I avoid sending "surfacePoint" to interface?
	public Maybe<SurfacePoint> GetSurfacePointUnderCursor() // TODO: GetSimulationObject
	{
		RaycastHit hit;
		if (InputManager.TryGetRaycastHit(out hit))
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

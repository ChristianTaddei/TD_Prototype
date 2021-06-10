using System;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrainState : InterfaceState
{
	private Interface _interface;

	private ModifyTerrainCommand modifyTerrainCommand;

	private RaycastMediator raycastMediator;
	private HighlightMediator highlightMediator;

	// public float BrushRadius { get; set; } = 2.0f;
	public float HeightChange { get; set; } = 1.0f;


	public ModifyTerrainState(Interface _interface, ModifyTerrainCommand modifyTerrainCommand, RaycastMediator raycastMediator, HighlightMediator highlightMediator)
	{
		this._interface = _interface;

		this.modifyTerrainCommand = modifyTerrainCommand;

		this.raycastMediator = raycastMediator;
		this.highlightMediator = highlightMediator;

		updateCommand();
	}


	public override void Mount()
	{


		// _interface.OnSelect = modifyTerrainCommand;
		_interface.OnHover = () =>
		{
			// Debug.Log("hovering from modify terrain");

			Maybe<SurfacePoint> targetSP = raycastMediator.GetSurfacePointUnderCursor();
			if (targetSP.HasValue())
			{
				highlightMediator.Highlight(targetSP.Value.Position);
			}
		};
	}

	public override void Update()
	{
		updateCommand();

		highlightMediator.ClearHighlights();
	}

	public override void Unmount()
	{
		highlightMediator.ClearHighlights();
	}

	private void updateCommand()
	{
		modifyTerrainCommand.HeightChange = HeightChange;
	}
}

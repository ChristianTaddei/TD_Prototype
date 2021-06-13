using System;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrainState : InterfaceState
{
	private InterfaceManager _interface;

	private ModifyTerrainCommand modifyTerrainCommand;

	private RaycastMediator raycastMediator;
	private HighlightMediator highlightMediator;

	// public float BrushRadius { get; set; } = 2.0f;
	public float HeightChange { get; set; } = 1.0f;


	public ModifyTerrainState(InterfaceManager _interface, ModifyTerrainCommand modifyTerrainCommand, RaycastMediator raycastMediator, HighlightMediator highlightMediator)
	{
		this._interface = _interface;

		this.modifyTerrainCommand = modifyTerrainCommand;

		this.raycastMediator = raycastMediator;
		this.highlightMediator = highlightMediator;

		updateCommand();
	}

	private Maybe<SurfacePoint> target;
	public override void Mount()
	{
		_interface.OnSelect = () =>
		{
			if (target.HasValue())
			{
				modifyTerrainCommand.TargetFace = target.Value.Face;
				modifyTerrainCommand.Execute();
			}
		};

		_interface.OnHover = () =>
		{
			target = raycastMediator.GetSurfacePointUnderCursor();
			if (target.HasValue())
			{
				highlightMediator.Highlight(target.Value.Position);
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

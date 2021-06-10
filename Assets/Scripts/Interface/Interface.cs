using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface
{
	public static Interface Instance; // Only used to quickly bind from UIBuilder

	public Action OnSelect { get; set; }
	public Action OnHover { get; set; }

	// private readonly SelectUnitsCommand selectUnitsCommand;
	// private readonly MoveUnitsCommand moveUnitsCommand;

	public readonly ModifyTerrainState ModifyTerrainState; // TODO: create "Menu" here and pass fields like the rest
	public readonly MakePathState MakePathState;

	private InterfaceState defaultInterfaceState;
	private InterfaceState activeInterfaceState;

	public Interface(RaycastMediator raycastMediator, HighlightMediator highlightMediator, ModifyTerrainCommand modifyTerrainCommand)
	{
		Instance = this;

		OnSelect = () => {Debug.Log("Empty select called");};
		OnHover = () => {Debug.Log("Empty hover called");};

		// MakePathState = new MakePathState(
		//     this,
		//     highlightCommand
		// );

		ModifyTerrainState = new ModifyTerrainState(
		    this,
		    modifyTerrainCommand,
            raycastMediator,
            highlightMediator // TODO: not shared, to each state his?
		);

		defaultInterfaceState = ModifyTerrainState;
		SetState(defaultInterfaceState);
	}

	public void Update()
	{
		activeInterfaceState.Update();

        OnHover();
	}

	internal void ResetDefaultState()
	{
		this.SetState(defaultInterfaceState);
	}

	// TODO: Property instead?
	public void SetState(InterfaceState state)
	{
		if (activeInterfaceState != state)
		{
			if (activeInterfaceState != null) activeInterfaceState.Unmount();
			activeInterfaceState = state;
			state.Mount();
		}
	}

	private List<GameObject> debugLines = new List<GameObject>();

	// Actions
	public void EnterEditMode()
	{
		ResetDefaultState();
		// SimulationRepresentation.Instance.RepresentationRunning = false;
		// SimulationRepresentation.Instance.RepresentationPaused = true;

		// editToolBar.show(true);
		// topBar.EditButton.GetComponent<Button>().GetComponent<Image>().color = Color.yellow;

		// playToolBar.show(false);
		// topBar.PlayButton.GetComponent<Button>().GetComponent<Image>().color = Color.white;
	}

	public void EnterPlayMode()
	{
		ResetDefaultState();
		// SimulationRepresentation.Instance.RepresentationRunning = true;
		// SimulationRepresentation.Instance.RepresentationPaused = false;
		// Simulation.Instance.CurrentStateModified();

		// playToolBar.show(true);
		// topBar.PlayButton.GetComponent<Button>().GetComponent<Image>().color = Color.yellow;

		// editToolBar.show(false);
		// topBar.EditButton.GetComponent<Button>().GetComponent<Image>().color = Color.white;
	}
}

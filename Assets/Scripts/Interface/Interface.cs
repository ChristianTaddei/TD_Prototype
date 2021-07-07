using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager
{
	public static InterfaceManager Instance; // Only used to quickly bind from UIBuilder

	public Action OnSelect { get; set; }
	public Action OnHover { get; set; }

	private InterfaceState state;
	public InterfaceState State
	{
		private get => state;

		set
		{
			if (state != value)
			{
				if (state != null) { state.Unmount(); }
				state = value;
				state.Mount();
			}
		}
	}

	private InterfaceState defaultInterfaceState;

	// private readonly SelectUnitsCommand selectUnitsCommand;
	// private readonly MoveUnitsCommand moveUnitsCommand;

	private Menu menu;

	// public readonly ModifyTerrainState ModifyTerrainState;
	// public readonly MakePathState MakePathState;


	// public InterfaceManager(RaycastMediator raycastMediator, HighlightMediator highlightMediator, ModifyTerrainCommand modifyTerrainCommand)
	// {
	// 	OnSelect = () => { Debug.Log("Empty select called"); };
	// 	OnHover = () => { Debug.Log("Empty hover called"); };

	// 	menu = GameObject.Find("UI").AddComponent<Menu>(); // TODO: create UIDoc and EvtSys here too?
	// 	menu.interfaceManager = this;

	// 	// MakePathState = new MakePathState(
	// 	//     this,
	// 	//     highlightCommand
	// 	// );

	// 	ModifyTerrainState = new ModifyTerrainState(
	// 	    this,
	// 	    modifyTerrainCommand,
	//     raycastMediator,
	//     highlightMediator // TODOHIGH: not shared, to each state his?
	// 	);

	// 	defaultInterfaceState = ModifyTerrainState;
	// 	State = defaultInterfaceState;
	// }

	public void Update()
	{
		State.Update();

		OnHover();
	}

	internal void ResetDefaultState()
	{
		State = defaultInterfaceState;
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

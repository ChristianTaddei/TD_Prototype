using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager
{
    private InterfaceState defaultInterfaceState;

    private InterfaceState activeInterfaceState;

    private readonly ModifyTerrainState modifyTerrainState;
    private readonly MakePathState makePathState;

    public InterfaceManager(ModifyTerrainCommand modifyTerrainCommand)
    {
        makePathState = new MakePathState();
        modifyTerrainState = new ModifyTerrainState(modifyTerrainCommand);

        defaultInterfaceState = makePathState;
        SetState(defaultInterfaceState);
    }
    
    public void Update(){
        activeInterfaceState.Update();
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
        // RepresentationManager.Instance.RepresentationRunning = false;
        // RepresentationManager.Instance.RepresentationPaused = true;

        // editToolBar.show(true);
        // topBar.EditButton.GetComponent<Button>().GetComponent<Image>().color = Color.yellow;

        // playToolBar.show(false);
        // topBar.PlayButton.GetComponent<Button>().GetComponent<Image>().color = Color.white;
    }

    public void EnterPlayMode()
    {
        ResetDefaultState();
        // RepresentationManager.Instance.RepresentationRunning = true;
        // RepresentationManager.Instance.RepresentationPaused = false;
        // SimulationManager.Instance.CurrentStateModified();

        // playToolBar.show(true);
        // topBar.PlayButton.GetComponent<Button>().GetComponent<Image>().color = Color.yellow;

        // editToolBar.show(false);
        // topBar.EditButton.GetComponent<Button>().GetComponent<Image>().color = Color.white;
    }
}

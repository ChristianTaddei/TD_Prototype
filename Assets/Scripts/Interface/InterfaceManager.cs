using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager
{
    private TopBar topBar;
    private EditToolBar editToolBar;
    private PlayToolBar playToolBar;

    public InterfaceManager(Command modifyTerrainHeight)
    {
        topBar = new TopBar();

        editToolBar = new EditToolBar(modifyTerrainHeight);
        editToolBar.show(false);

        playToolBar = new PlayToolBar();
        playToolBar.show(false);
    }

    // Actions
    public void EnterEditMode()
    {
        // RepresentationManager.Instance.RepresentationRunning = false;
        // RepresentationManager.Instance.RepresentationPaused = true;

        editToolBar.show(true);
        topBar.EditButton.GetComponent<Button>().GetComponent<Image>().color = Color.yellow;

        playToolBar.show(false);
        topBar.PlayButton.GetComponent<Button>().GetComponent<Image>().color = Color.white;
    }

    public void EnterPlayMode()
    {
        // RepresentationManager.Instance.RepresentationRunning = true;
        // RepresentationManager.Instance.RepresentationPaused = false;
        // SimulationManager.Instance.CurrentStateModified();

        playToolBar.show(true);
        topBar.PlayButton.GetComponent<Button>().GetComponent<Image>().color = Color.yellow;

        editToolBar.show(false);
        topBar.EditButton.GetComponent<Button>().GetComponent<Image>().color = Color.white;
    }

    internal void ShowSaveAsPopup()
    {
        SaveAsPopup popup = new SaveAsPopup();
    }
}

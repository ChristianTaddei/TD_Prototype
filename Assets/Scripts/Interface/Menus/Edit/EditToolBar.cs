using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditToolBar : VerticalMenu
{
    private SimulationManager simulationManager;

    private GameObject clearButton;
    private GameObject terrainButton;
    private GameObject placeButton;
    private GameObject geometryButton;

    private TerrainMenu terrainMenu;
    private PlaceMenu placeMenu;
    private GeometryMenu geometryMenu;

    public EditToolBar()
           : base("Edit ToolBar", Anchor.TOP_LEFT, 5, 5, 100, 30)
    {
        simulationManager = GameObject.Find("GameManager").GetComponent<SimulationManager>();

        // TODO: take top bar height instead
        moveVerticalPosition(-verticalPadding - 2 * buttonHeight);

        clearButton = addButton(
            "Clear",
            () =>
            {
                interfaceManager.ResetDefaultState();
                hideSecondaryMenus();
            });

        // clearButton = addButton(
        //     "Save",
        //     () =>
        //     {
        //         interfaceManager.ShowSaveAsPopup();
        //     });

        ModifyTerrain modifyTerrain = new ModifyTerrain();
        terrainMenu = new TerrainMenu(modifyTerrain, 2 * horizontalPadding + buttonWidth, currentVerticalPosition);
        terrainMenu.show(false);
        terrainButton = addButton(
            "Terrain",
            () =>
            {
                hideSecondaryMenus();
                terrainMenu.show(true);
                interfaceManager.SetState(modifyTerrain);
            }
            );

        placeMenu = new PlaceMenu(2 * horizontalPadding + buttonWidth, currentVerticalPosition);
        placeMenu.show(false);
        placeButton = addButton(
            "Place",
            () =>
            {
                interfaceManager.ResetDefaultState();
                hideSecondaryMenus();
                placeMenu.show(true);
            }
            );

        geometryMenu = new GeometryMenu(2 * horizontalPadding + buttonWidth, currentVerticalPosition);
        geometryMenu.show(false);
        geometryButton = addButton(
            "Geometry",
            () =>
            {
                interfaceManager.ResetDefaultState();
                hideSecondaryMenus();
                geometryMenu.show(true);
            }
            );
    }

    private void hideSecondaryMenus()
    {
        terrainMenu.show(false);
        placeMenu.show(false);
        geometryMenu.show(false);
    }

    public new void show(bool show)
    {
        base.show(show);
        hideSecondaryMenus();
    }
}

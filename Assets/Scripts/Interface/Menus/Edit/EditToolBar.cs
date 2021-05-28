using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditToolBar : VerticalMenu
{
    private GameObject clearButton;
    private GameObject terrainButton;
    private GameObject placeButton;

    private TerrainMenu terrainMenu;
    private PlaceMenu placeMenu;

    public EditToolBar(Command modifyTerrainHeight)
           : base("Edit ToolBar", Anchor.TOP_LEFT, 5, 5, 100, 30)
    {
        // TODO: take top bar height instead
        MoveVerticalPosition(-verticalPadding - 2 * buttonHeight);

        clearButton = AddButton(
            "Clear",
            () =>
            {
                // interfaceManager.ResetDefaultState();
                hideSecondaryMenus();
            });

        // clearButton = addButton(
        //     "Save",
        //     () =>
        //     {
        //         interfaceManager.ShowSaveAsPopup();
        //     });

        terrainMenu = new TerrainMenu(modifyTerrainHeight, 2 * horizontalPadding + buttonWidth, currentVerticalPosition);
        terrainMenu.show(false);
        terrainButton = AddButton(
            "Terrain",
            () =>
            {
                hideSecondaryMenus();
                terrainMenu.show(true);
                // interfaceManager.SetState(modifyTerrain);
            }
            );

        placeMenu = new PlaceMenu(2 * horizontalPadding + buttonWidth, currentVerticalPosition);
        placeMenu.show(false);
        placeButton = AddButton(
            "Place",
            () =>
            {
                // interfaceManager.ResetDefaultState();
                hideSecondaryMenus();
                placeMenu.show(true);
            }
            );

    }

    private void hideSecondaryMenus()
    {
        terrainMenu.show(false);
        placeMenu.show(false);
    }

    public new void show(bool show)
    {
        base.show(show);
        hideSecondaryMenus();
    }
}

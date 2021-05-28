using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayToolBar : VerticalMenu
{
    private GameObject playToolBarGameObject;
    private GameObject clearButton;
    private GameObject findPathButton;
    private GameObject simulationOptionsButton;

    private SimulationOptionsMenu simulationOptionsMenu;

    public PlayToolBar() : base("Play ToolBar", Anchor.TOP_RIGHT, 5, 5, 100, 30)
    {
        // TODO: take top bar height instead
        MoveVerticalPosition(-verticalPadding - 2 * buttonHeight);

        clearButton = AddButton(
           "Clear",
            () =>
            {
                hideSecondaryMenus();
                // interfaceManager.ResetDefaultState();
            }
            );

        // FindPath fp = new FindPath();
        findPathButton = AddButton(
           "Find Path",
            () =>
            {
                hideSecondaryMenus();
                // interfaceManager.SetState(fp);
            }
            );

        simulationOptionsMenu = new SimulationOptionsMenu(2 * horizontalPadding + buttonWidth, currentVerticalPosition);
        simulationOptionsMenu.show(false);
        simulationOptionsButton = AddButton(
            "Simulation",
            () =>
            {
                hideSecondaryMenus();
                simulationOptionsMenu.show(true);
            }
            );

    }

    private void hideSecondaryMenus()
    {
        simulationOptionsMenu.show(false);
    }

    public new void show(bool show)
    {
        base.show(show);
        hideSecondaryMenus();
    }
}

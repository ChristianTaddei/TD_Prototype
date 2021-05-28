using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMenu : VerticalMenu
{
    public PlaceMenu(float horizontalPadding, float verticalStartingPosition)
    : base("Place Menu", Anchor.TOP_LEFT, horizontalPadding, 5, 100, 30)
    {
        MoveVerticalPosition(verticalStartingPosition);

        // PlaceTower placeTower = new PlaceTower();
        AddButton(
            "Tower",
            () =>
            {
                // interfaceManager.SetState(placeTower);
            }

        );

        // PlaceObjective placeObjective = new PlaceObjective();
        AddButton(
            "Objective",
            () =>
            {
                // interfaceManager.SetState(placeObjective); 
            }
        );

        AddButton(
            "Spawner",
            () =>
            {
                // interfaceManager.ResetDefaultState(); 
            }
        );
    }
}

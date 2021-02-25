using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMenu : VerticalMenu
{
    public PlaceMenu(float horizontalPadding, float verticalStartingPosition)
    : base("Place Menu", Anchor.TOP_LEFT, horizontalPadding, 5, 100, 30)
    {
        moveVerticalPosition(verticalStartingPosition);

        PlaceTower placeTower = new PlaceTower();
        addButton(
            "Tower",
            () => { interfaceManager.SetState(placeTower); }
        );

        PlaceObjective placeObjective = new PlaceObjective();
        addButton(
            "Objective",
            () => { interfaceManager.SetState(placeObjective); }
        );

        addButton(
            "Spawner",
            () => { interfaceManager.ResetDefaultState(); }
        );
    }
}

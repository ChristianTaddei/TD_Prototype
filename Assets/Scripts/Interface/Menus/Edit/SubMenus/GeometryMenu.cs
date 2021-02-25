using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryMenu : VerticalMenu
{
    public GeometryMenu(float horizontalPadding, float verticalStartingPosition)
    : base("Geometry Menu", Anchor.TOP_LEFT, horizontalPadding, 5, 100, 30)
    {
        moveVerticalPosition(verticalStartingPosition);

        TraceLine traceLine = new TraceLine();
        addButton(
            "Line",
            () => { interfaceManager.SetState(traceLine); }
        );
    }
}



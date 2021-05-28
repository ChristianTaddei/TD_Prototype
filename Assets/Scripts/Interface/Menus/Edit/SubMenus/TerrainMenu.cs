using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMenu : VerticalMenu
{
    private Command raiseTerrain;

    private float heightChange = 1.0f;
    private float brushRadius = 3.0f;

    public TerrainMenu(Command raiseTerrain, float horizontalPadding, float verticalStartingPosition)
        : base("Terrain Menu", Anchor.TOP_LEFT, horizontalPadding + 5, 5, 100, 40)
    {
        this.raiseTerrain = raiseTerrain;

        SetupInterface(verticalStartingPosition);
    }

    private void SetupInterface(float verticalStartingPosition)
    {
        MoveVerticalPosition(verticalStartingPosition);

        AddSlider(
            "Height",
            -5, 5,
            (float value) =>
            {
                heightChange = value;
            }
        );

        AddSlider(
            "Radius",
            1, 10,
            (float value) =>
            {
                brushRadius = value;
            }
        );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMenu : VerticalMenu
{
    ModifyTerrain modifyTerrain;

    public TerrainMenu(ModifyTerrain modifyTerrain, float horizontalPadding, float verticalStartingPosition)
    : base("Terrain Menu", Anchor.TOP_LEFT, horizontalPadding + 5, 5, 100, 40)
    {
        this.modifyTerrain = modifyTerrain;
        moveVerticalPosition(verticalStartingPosition);

        addSlider(
            "Height",
            -5, 5,
            (float value) =>
            {
                modifyTerrain.HeightChange = value;
            }
        );

        addSlider(
            "Radius",
            1, 10,
            (float value) =>
            {
                modifyTerrain.Radius = value;
            }
        );
    }
}

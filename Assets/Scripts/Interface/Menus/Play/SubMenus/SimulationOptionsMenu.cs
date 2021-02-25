using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationOptionsMenu : VerticalMenu
{
    private float lastSpeed = 1.0f;

    public SimulationOptionsMenu(float horizontalPadding, float verticalStartingPosition)
    : base("Options", Anchor.TOP_RIGHT, horizontalPadding + 5, 5, 230, 40)
    {
        moveVerticalPosition(verticalStartingPosition);

        // addLabel("Ready: Current:", () =>
        // {
        //     return "Buffered Future States: " + SimulationManager.Instance.ReadyStates.Count;
        // });

        
        addButton("Play/Stop", () =>
        {
            if (RepresentationManager.Instance.RepresentationPaused)
            {
                RepresentationManager.Instance.RepresentationSpeed = lastSpeed;
            } else {
                lastSpeed = RepresentationManager.Instance.RepresentationSpeed;
                RepresentationManager.Instance.RepresentationSpeed = 0.0f;
            }

            RepresentationManager.Instance.RepresentationPaused = !RepresentationManager.Instance.RepresentationPaused;

        });

        addButton("Speed 0.5x", () =>
        {
            RepresentationManager.Instance.RepresentationSpeed = 0.5f;
        });

        addButton("Speed 1.0x", () =>
        {
            RepresentationManager.Instance.RepresentationSpeed = 1.0f;
        });

        addButton("Speed 2.0x", () =>
        {
            RepresentationManager.Instance.RepresentationSpeed = 2.0f;
        });

        addButton("Speed 5.0x", () =>
        {
            RepresentationManager.Instance.RepresentationSpeed = 5.0f;
        });
    }
}

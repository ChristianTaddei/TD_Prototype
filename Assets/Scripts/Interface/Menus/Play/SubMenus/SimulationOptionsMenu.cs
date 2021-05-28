using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationOptionsMenu : VerticalMenu
{
    int i = 0;
    public SimulationOptionsMenu(float horizontalPadding, float verticalStartingPosition)
    : base("Options", Anchor.TOP_RIGHT, horizontalPadding + 5, 5, 230, 40)
    {
        MoveVerticalPosition(verticalStartingPosition);

        AddLabel("Ready: Current:", () =>
        {
            return "Buffered Future States: "/* + SimulationManager.Instance.ReadyStates.Count((s) => true)*/;
        });

        AddButton("Play/Stop", () => {
            // RepresentationManager.Instance.RepresentationPaused = !RepresentationManager.Instance.RepresentationPaused;
        });

        AddButton("Speed 0.5x", () => {
            // RepresentationManager.Instance.representationStepTotalTime = SimulationManager.SimulationStepDuration / 0.5f;
        });
    
        AddButton("Speed 1.0x", () => {
            // RepresentationManager.Instance.representationStepTotalTime = SimulationManager.SimulationStepDuration / 1.0f;
        });
    
        AddButton("Speed 2.0x", () => {
            // RepresentationManager.Instance.representationStepTotalTime = SimulationManager.SimulationStepDuration / 2.0f;
        });

        AddButton("Speed 5.0x", () => {
            // RepresentationManager.Instance.representationStepTotalTime = SimulationManager.SimulationStepDuration / 5.0f;
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAsPopup : UIElement
{
    private SimulationManager simulationManager;

    public SaveAsPopup() : base("SaveAsPopup")
    {
                simulationManager = GameObject.Find("GameManager").GetComponent<SimulationManager>();

        InterfaceFactory.createPopup(
            "SaveAsPopup",
            this.canvas,
            new Vector2(0, 0),
            Anchor.CENTRE,
            (string s) =>
            {
                Debug.Log(s + " saved");
                // simulationManager.SaveBoard(s);
            });
    }
}

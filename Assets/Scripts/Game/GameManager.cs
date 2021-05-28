using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InterfaceManager interfaceManager;

    void Start()
    {
        Surface surface = new Surface(10.0f);

        interfaceManager = new InterfaceManager(
            new ModifyTerrainCommand(surface)
            );

        Board board = new Board(surface);
        BoardRepresentation.MakeFrom(board);
    }

    void Update()
    {
        interfaceManager.Update();
    }
}

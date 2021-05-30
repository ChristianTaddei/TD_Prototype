using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InterfaceManager InterfaceManager;

    void Start()
    {
        // TODO: add components here to have correct order

        Surface surface = new Surface(10.0f);

        InterfaceManager = new InterfaceManager(
            new ModifyTerrainCommand(surface)
            );

        Board board = new Board(surface);
        BoardRepresentation.MakeFrom(board);

    }

    void Update()
    {
        InterfaceManager.Update();
    }
}

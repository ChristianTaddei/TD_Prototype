using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    InterfaceManager interfaceManager;

    void Start()
    {
        interfaceManager = GetComponent<InterfaceManager>();

        Surface surface = new Surface(10.0f);
        
        ModifyTerrainHeight modifyTerrainHeight = new ModifyTerrainHeight(surface);
        interfaceManager.SetModifyTerrainCommand(modifyTerrainHeight);

        Board board = new Board(surface);
        BoardRepresentation.MakeFrom(board);
    }

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Surface surface = new Surface(10.0f);

        ModifyTerrainHeight modifyTerrainHeight = new ModifyTerrainHeight(surface);

        InterfaceManager interfaceManage = new InterfaceManager(modifyTerrainHeight);

        Board board = new Board(surface);
        BoardRepresentation.MakeFrom(board);
    }

    void Update()
    {

    }
}

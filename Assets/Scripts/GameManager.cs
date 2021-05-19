using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
       Surface surface = new Surface(10.0f);
       Board board = new Board(surface);
       BoardRepresentation.MakeFrom(board); 
    }

    void Update()
    {
        
    }
}

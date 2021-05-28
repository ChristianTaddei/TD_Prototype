using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCell : InterfaceState
{
    public void Mount() { }

    public void Unmount() { }
/* FIX
    public void OnCellSelected(Vertex selectedCell)
    {
        // TODO: move to simulationManager
        GameObject.Destroy(selectedCell.representation);
        board.Cells.Remove(selectedCell.BoardPosition);
        //board.updateDistances();
    }
*/
    public void Update()
    {

    }
}

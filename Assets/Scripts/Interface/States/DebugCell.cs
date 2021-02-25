using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCell : InterfaceState
{
    private Vertex lastSelectedCell;

    public void Mount() { }
/* FIX
    public void Unmount()
    {
        resetLastCell();
    }
    
    public void OnCellSelected(Vertex selectedCell)
    {
        resetLastCell();

        selectedCell.representation.gameObject.transform.Find("tall_cube_border")
            .gameObject.GetComponent<Renderer>().material.color = Color.red;

        PrintInfo(selectedCell);

        lastSelectedCell = selectedCell;
    }

    public void Update()
    {

    }

    private void resetLastCell()
    {
        if (lastSelectedCell != null && lastSelectedCell.representation != null)
        {
            lastSelectedCell.representation.gameObject.transform.Find("tall_cube_border")
                .gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    private void PrintInfo(Vertex cell)
    {
        Debug.Log("*****");
        
        float threath;
        if (board.Threaths.TryGetValue(cell.BoardPosition, out threath))
        {
            Debug.Log("threath: " + threath);
        }

        foreach (Cardinal direction in Cardinals.Values())
        {
            float distance;
            if(board.EuclideanDistances.TryGetValue(cell.BoardPosition, direction, out distance))
            Debug.Log(direction + " " + distance);
        }

    }
*/
}

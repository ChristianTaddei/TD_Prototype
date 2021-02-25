
using System.Collections.Generic;
using UnityEngine;

public class TraceLine : InterfaceState
{
    List<GameObject> placeholders;
    SurfacePoint lineStart, lineEnd;

    public TraceLine()
    {
        placeholders = new List<GameObject>();
    }

    public override void Update()
    {
        SurfacePoint clickedBoardPosition;
        if (InputManager.Instance.TryGetBoardPositionUnderCursor(out clickedBoardPosition))
        {
            if (InputManager.Instance.LeftClick())
            {
                if (lineStart == null)
                {
                    // lineStart = new SurfacePoint(clickedBoardPosition);

                    placeholders.Add(InterfaceManager.Instance.MakeHighlight(
                        clickedBoardPosition,
                        HighlightSize.Small,
                        Color.green
                    ));
                }
                else if (lineEnd == null)
                {
                    // lineEnd = new BarycentricPoint(clickedBoardPosition);

                    // placeholders.Add(InterfaceManager.Instance.MakeHighlight(
                    //      clickedBoardPosition,
                    //      HighlightSize.Small,
                    //      Color.green
                    //  ));

                    // SurfaceLine line = new SurfaceLine(lineStart, lineEnd);

                    ClearPlaceholders();
                    // placeholders.AddRange(InterfaceManager.Instance.MakeHighlight(line));
                }
                else
                {
                    lineStart = null;
                    lineEnd = null;
                    ClearPlaceholders();
                }
            }
        }
    }

    private void ClearPlaceholders()
    {
        foreach (GameObject placeholder in placeholders)
        {
            GameObject.Destroy(placeholder);
        }

        placeholders.Clear();
    }

    public override void Unmount()
    {
        ClearPlaceholders();
    }
}

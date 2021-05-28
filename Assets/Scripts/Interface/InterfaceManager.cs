using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;

    private InputManager inputManager;

    private ModifyTerrainHeight modifyTerrainHeight;

    void Start()
    {
        Instance = this;

        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
    }

    public void SetModifyTerrainCommand(ModifyTerrainHeight modifyTerrainHeight)
    {
        this.modifyTerrainHeight = modifyTerrainHeight; 
        inputManager.ClickCommand = modifyTerrainHeight; // TODO: start actions order
    }

    private void MakePath()
    {
        // Maybe<SurfacePoint> maybeSurfacePoint = GetSurfacePointUnderCursor();
        // if (maybeSurfacePoint.HasValue())
        // {
        //     SurfacePoint newP = maybeSurfacePoint.Value;
        //     if (oldP == null)
        //     {
        //         oldP = newP;
        //     }

        //     else
        //     {
        //         foreach (GameObject m in markers)
        //         {
        //             GameObject.Destroy(m);
        //         }
        //         markers.Clear();

        //         Maybe<SurfacePath> path = newP.Face.Surface.MakeDirectPath(oldP, newP);
        //         if (path.HasValue())
        //         {
        //             foreach (SurfacePoint pathPoint in path.Value.Points)
        //             {
        //                 markers.Add(Instantiate(marker, pathPoint.Position, Quaternion.identity));
        //             }
        //         }
        //         else
        //         {
        //             Debug.Log("failed to make path");
        //         }

        //         oldP = null;
        //     }
        // }
        // else
        // {
        //     Debug.Log("Failed to make SP under cursor");
        // }
    }
}

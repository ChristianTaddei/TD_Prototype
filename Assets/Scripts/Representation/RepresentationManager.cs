using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public enum HighlightSize
{
    Small,
    Large,
    VerySmall,
}

public class RepresentationManager : MonoBehaviour
{
    public static RepresentationManager Instance;

    private Dictionary<Type, GameObject> loadedPrefabs = new Dictionary<Type, GameObject>();

    // private HashSet<IRepresentable> representedObjects;

    public bool RepresentationRunning { get; set; }
    public bool RepresentationPaused { get; set; }

    public float RepresentationStepElapsedFraction { get => representationStepCurrentTime / representationStepTotalTime; }

    public float representationStepTotalTime = 0.5f;
    private float representationStepCurrentTime = 0.0f;

    void Start()
    {
        Instance = this;
        RepresentationRunning = false;
        RepresentationPaused = true;

        // representedObjects = new HashSet<IRepresentable>();
    }

    void Update()
    {

    }

    public IEnumerable<GameObject> HighlightSurfacePoints(List<SurfacePoint> sps, HighlightSize size, Color color)
    {
        List<GameObject> gos = new List<GameObject>();

        foreach (SurfacePoint sp in sps)
        {
            gos.Add(MakeHighlight(sp, size, color));
        }
        
        return gos;
    }

    public GameObject MakeHighlight(SurfacePoint sp, HighlightSize size, Color color)
    {
        return MakeHighlight(sp.Position, size, color);
    }

    public GameObject MakeHighlight(Vector3 position, HighlightSize size, Color color)
    {
        GameObject representationGameObject = (GameObject)GameObject
            .Instantiate((GameObject)Resources.Load("Prefabs/Marker"));

        representationGameObject.transform.position = position;

        if (size == HighlightSize.Large)
        {
            representationGameObject.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }
        else if (size == HighlightSize.VerySmall)
        {
            representationGameObject.transform.localScale = new Vector3(.2f, .2f, .2f);
        }

        representationGameObject.GetComponent<Renderer>().material.color = color;

        return representationGameObject;
    }

    internal void MakeMarker(Vector3 point)
    {
        MakeMarker(point);
    }
}

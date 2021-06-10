using System;
using System.Collections.Generic;
using UnityEngine;

public enum HighlightSize
{
    Small,
    Large,
    VerySmall,
}

public class RepresentationFactory
{
    private static Dictionary<Type, GameObject> loadedPrefabs = new Dictionary<Type, GameObject>();

    public static GameObject GetPrefab(Type type)
    {
        GameObject prefab;
        if (!loadedPrefabs.TryGetValue(type, out prefab))
        {
            loadedPrefabs[type] = loadPrefab(type);
            prefab = loadedPrefabs[type];
        }

        return prefab;
    }

    private static GameObject loadPrefab(Type type)
    {
        // if (type == typeof(Tower))
        // {
        //     return (GameObject)Resources.Load("Prefabs/Tower");
        // }
        // else if (type == typeof(Objective))
        // {
        //     return (GameObject)Resources.Load("Prefabs/Objective");
        // }
        // else if (type == typeof(Enemy))
        // {
        //     return (GameObject)Resources.Load("Prefabs/Enemy");
        // }
        // else if (type == typeof(HitTrajectory))
        // {
        //     return (GameObject)Resources.Load("Prefabs/HitTrajectory");
        // }
        // else
        // {
        return (GameObject)Resources.Load("Prefabs/Marker");
        // }
    }

    //     public static IRepresentation AddRepresentationComponent(GameObject gameObject, Type type)
    //     {
    //         if (type == typeof(Tower))
    //         {
    //             return gameObject.AddComponent<TowerRepresentation>();
    //         }
    //         else if (type == typeof(Objective))
    //         {
    //             return gameObject.AddComponent<ObjectiveRepresentation>();
    //         }
    //         else if (type == typeof(Enemy))
    //         {
    //             return gameObject.AddComponent<EnemyRepresentation>();
    //         }
    //         else if (type == typeof(Board))
    //         {
    //             return gameObject.AddComponent<TerrainRepresentation>();
    //         }

    //         // TODO: marker as default
    //         return gameObject.AddComponent<ObjectiveRepresentation>();

    //     }

    // public static IEnumerable<GameObject> HighlightSurfacePoints(List<SurfacePoint> sps, HighlightSize size, Color color)
    // {
    //     List<GameObject> gos = new List<GameObject>();

    //     foreach (SurfacePoint sp in sps)
    //     {
    //         gos.Add(HighlightSurfacePoint(sp, size, color));
    //     }

    //     return gos;
    // }

    // public static GameObject HighlightSurfacePoint(SurfacePoint sp, HighlightSize size, Color color)
    // {
    //     return HighlightPoint(sp.Position, size, color);
    // }

    public  GameObject HighlightPoint(Vector3 position, HighlightSize size = HighlightSize.Small)
    {
        GameObject instantiatedMarker = (GameObject)GameObject
            .Instantiate((GameObject)Resources.Load("Prefabs/Marker"));

        instantiatedMarker.transform.position = position;

        if (size == HighlightSize.Large)
        {
            instantiatedMarker.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }
        else if (size == HighlightSize.VerySmall)
        {
            instantiatedMarker.transform.localScale = new Vector3(.2f, .2f, .2f);
        }

        instantiatedMarker.GetComponent<Renderer>().material.color = Color.gray;

        return instantiatedMarker;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class RepresentationFactory
{
    // TODO: move all to representationFactory
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
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class RepresentationFactory
{

    private static Dictionary<String, GameObject> loadedPrefabs = new Dictionary<String, GameObject>();
    public static GameObject GetPrefab(String s)
    {
        GameObject prefab;
        if (!loadedPrefabs.TryGetValue(s, out prefab))
        {
            loadedPrefabs[s] = loadPrefab(s);
            prefab = loadedPrefabs[s];
        }

        return prefab;
    }

    private static GameObject loadPrefab(String s)
    {
        if (s == "tower")
        {
            return (GameObject)Resources.Load("Prefabs/Tower");
        }
        else if (s == "objective")
        {
            return (GameObject)Resources.Load("Prefabs/Objective");
        }
        else if (s == "enemy")
        {
            return (GameObject)Resources.Load("Prefabs/Enemy");
        }
        else if (s == "hit")
        {
            return (GameObject)Resources.Load("Prefabs/HitTrajectory");
        }
        else
        {
            return (GameObject)Resources.Load("Prefabs/Marker");
        }
    }

    public static IRepresentation AddRepresentationComponent(GameObject gameObject, Type type)
    {
        if (type == typeof(Tower))
        {
            return gameObject.AddComponent<TowerRepresentation>();
        }
        else if (type == typeof(Objective))
        {
            return gameObject.AddComponent<ObjectiveRepresentation>();
        }
        else if (type == typeof(Enemy))
        {
            return gameObject.AddComponent<EnemyRepresentation>();
        }
        else if (type == typeof(Surface))
        {
            return gameObject.AddComponent<BoardRepresentation>();
        }

        // TODO: marker as default
        return gameObject.AddComponent<ObjectiveRepresentation>();

    }

    public static void MakeRepresentationFor(IRepresentable representable)
    {
        GameObject representationGameObject = (GameObject)GameObject
                    .Instantiate(RepresentationFactory.GetPrefab(representable.PrefabString));

        representable.Representation = representationGameObject.GetComponent<IRepresentation>();
        representable.Representation.RepresentedObject = representable;
    }
}
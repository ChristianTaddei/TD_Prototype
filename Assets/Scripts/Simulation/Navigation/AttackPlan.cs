using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlan
{
    private SimulationManager simulationManager;
    private RepresentationManager representationManager;

    private HashSet<Tower> engagedTowers;
    private Dictionary<Vertex, float> distances;
    private Dictionary<Vertex, float> allDistances;
    private int width;

    public Objective TargetObjective { get; set; }
    public List<Path> PathsToObjective { get; set; }
    public Dictionary<Vertex, float> AllDistances { get => allDistances; set => allDistances = value; }

    public static List<Vertex> alreadyUsedVertices = new List<Vertex>();

    public AttackPlan(SimulationState simulationState, Objective objective)
    {
        this.TargetObjective = objective;
        this.PathsToObjective = new List<Path>();
        this.engagedTowers = new HashSet<Tower>();

        List<Vertex> border = simulationState.BoardState.VertexStates.Keys.Where(v => v.Neighbours.Count < 8).ToList();
        int amountOfPaths = 5;
        for (int i = 0; i < amountOfPaths; i++)
        {
            PathsToObjective.ForEach(path => alreadyUsedVertices.AddRange(path.TraversedVertexs));

            SurfacePoint objectiveBP = simulationState.ObjectiveStates[objective].BoardPosition;

            Circle alwaysFreeArea = new Circle(
                simulationState.BoardState,
                objectiveBP,
                3.0f);
            alwaysFreeArea.Cells.ToList().ForEach(vertex => alreadyUsedVertices.Remove(vertex));

            Path newPath = SimulationManager.Instance.pathFinder.FindShortestPath(
                            simulationState.BoardState,objectiveBP.Face.a, border, alreadyUsedVertices);

            this.PathsToObjective.Add(newPath);
        }
    }
}

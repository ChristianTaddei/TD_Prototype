﻿using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Surface : Representable<BoardRepresentation>
{
    public override string PrefabString => "board";

    public Graph Graph { get; set; }

    public List<Vertex> Vertices;
    public int[] Triangles { get; private set; }
    private List<Face> faces;

    public BoardState InitalState { get; private set; }

    // *******************************************************

    public Surface(int edgeSize)
    {
        InitalState = new BoardState();
        // Vertices
        Vertices = new Vertex[edgeSize * edgeSize].ToList();

        // Create a vertex at every (i,j) integer position
        int vc = 0;
        for (int z = 0; z < edgeSize; z++)
        {
            for (int x = 0; x < edgeSize; x++)
            {
                Vertex newVertex = new Vertex();
                Vertices[x + (edgeSize * z)] = newVertex;
               
                InitalState.VertexStates.Add(newVertex, new VertexState(new Vector3(x, 0, z)));
            }
        }

        // Connect all vertices closer than 2.0f
        foreach (Vertex vertex in Vertices)
        {
            foreach (Vertex otherVertex in Vertices)
            {
                if (Vector3.Distance(
                        InitalState.VertexStates[vertex].Position,
                        InitalState.VertexStates[otherVertex].Position) < 2.0f
                    && !vertex.Equals(otherVertex))
                {
                    vertex.AddNeighbour(otherVertex);
                }
            }
        }

        // Mesh and faces
        Triangles = new int[(edgeSize - 1) * (edgeSize - 1) * 6];
        faces = new List<Face>();

        for (int i = 0; i < Vertices.Count; i++)
        {
            int row = i % edgeSize;
            int col = i / edgeSize;

            if (row < edgeSize - 1 && col < edgeSize - 1)
            {
                int triangleStartingIndex = (row + (col * (edgeSize - 1))) * 6;

                faces.Add(new Face(
                            Vertices[i],
                            Vertices[i + 1],
                            Vertices[i + edgeSize]));

                Triangles[triangleStartingIndex] = i;
                Triangles[triangleStartingIndex + 1] = i + edgeSize;
                Triangles[triangleStartingIndex + 2] = i + 1; 

                faces.Add(new Face(
                            Vertices[i + 1 + edgeSize],
                            Vertices[i + edgeSize],
                            Vertices[i + 1]));

                Triangles[triangleStartingIndex + 3] = i + 1 + edgeSize;
                Triangles[triangleStartingIndex + 4] = i + 1;
                Triangles[triangleStartingIndex + 5] = i + edgeSize; 
            }
        }
    }

    internal SurfacePoint MakeBPFrom2d(Vector3 destination2d)
    {
        foreach (Face face in faces)
        {
            SurfacePoint candidate = new SurfacePoint(
                InitalState,
                face,
                destination2d);

            if (candidate.Barycentrics.CheckInternal())
            {
                if(Vector3.Distance(destination2d, candidate.GetCartesians(InitalState)) > 0.1f){
                    // Debug.Log("makebp broke vector");
                }
                return candidate;
            }
        }

        Debug.Log("makeBP failed");
        return null;
    }

    public bool TryGetFaceFromIndex(int triangleIndex, out Face face)
    {
        // FIXME: what if faces does not have a value?
        if (triangleIndex < faces.Count)
        {
            face = faces[triangleIndex];
            return true;
        }

        face = default;
        return false;
    }

    public SurfacePoint SumBarAndCart(BoardState boardState, SurfacePoint boardPosition, Vector3 movementVector)
    {
        // Face face2d = ProjectFaceOn2D(boardPosition.Face);
        Vector3 destination = boardPosition.GetCartesians(boardState) + movementVector;
        Vector3 destination2d = new Vector3(
            destination.x, 0, destination.z);

        // BoardPosition flatBP = new BoardPosition(simState, face2d, destination2d);
        SurfacePoint flatBP = new SurfacePoint(SimulationManager.Instance.Board.InitalState, boardPosition.Face, destination2d);
        if (flatBP.Barycentrics.CheckInternal())
        {
            return new SurfacePoint(boardPosition.Face, flatBP.Barycentrics);
        }

        List<Face> farNeighbours = new List<Face>();
        foreach (Face nextFace in boardPosition.Face.GetNeighbourFaces())
        {
            // Face nextFace2d = ProjectFaceOn2D(nextFace);
            // flatBP = new BoardPosition(simState, nextFace2d, destination2d);
            flatBP = new SurfacePoint(SimulationManager.Instance.Board.InitalState, nextFace, destination2d);
            if (flatBP.Barycentrics.CheckInternal())
            {
                return new SurfacePoint(nextFace, flatBP.Barycentrics);
            }

            farNeighbours.AddRange(nextFace.GetNeighbourFaces());
        }

        foreach (Face nextFace in farNeighbours)
        {
            // Face nextFace2d = ProjectFaceOn2D(nextFace);
            // flatBP = new BoardPosition(simState, nextFace2d, destination2d);
            flatBP = new SurfacePoint(SimulationManager.Instance.Board.InitalState, nextFace, destination2d);
            if (flatBP.Barycentrics.CheckInternal())
            {
                return new SurfacePoint(nextFace, flatBP.Barycentrics);
            }
        }

        // TODO: make like tryGet
        Debug.Log("find next face failed");
        return null;
    }

    // private Face ProjectFaceOn2D(Face face)
    // {
    //     return new Face(
    //                  new Vertex(
    //                      new Vector3(
    //                         face.a.Position.x,
    //                         0,
    //                         face.a.Position.z)),
    //                  new Vertex(
    //                      new Vector3(
    //                         face.b.Position.x,
    //                         0,
    //                         face.b.Position.z)),
    //                  new Vertex(
    //                      new Vector3(
    //                         face.c.Position.x,
    //                         0,
    //                         face.c.Position.z)));
    // }

    // private static Vector3 ProjectVectorOnFace(Face face, Vector3 vector)
    // {
    //     Vector3 op = vector;
    //     Vector3 ap = face.a.Position;
    //     Vector3 n = Vector3.Cross(
    //         face.b.Position - face.a.Position,
    //         face.c.Position - face.a.Position);
    //     Vector3 projectedDest = op - (Vector3.Dot(ap, n) / n.sqrMagnitude) * n;
    //     return projectedDest;
    // }

    // public BoardPosition ToBoardPosition(Vertex vertex)
    // {
    //     Face foundFace = faces.First(face => face.a == vertex || face.b == vertex || face.c == vertex);
    //     return new BoardPosition(foundFace, vertex.Position);
    // }
}

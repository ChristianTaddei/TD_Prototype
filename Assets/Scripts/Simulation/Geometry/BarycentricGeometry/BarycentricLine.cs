using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarycentricLine
{
    public BarycentricVector Point { get; }
    public BarycentricVector Direction { get; }

    public BarycentricLine(BarycentricVector startPoint, BarycentricVector endPoint)
    {
        Point = startPoint;
        // Direction = (endPoint - Point).Normalized;
    }

    public List<BarycentricVector> GetIntersectionsWithTriangle(CartesianTriangle t)
    {
        // BarycentricLine lineOnBaseT = new BarycentricLine(Point.ChangeBase(t), (Point + Direction).ChangeBase(t));

        return null;
    }

    // public List<Face> GetTraversedFaces()
    // {
    //     // get intersection with start face
    //     // get next face from intersection coords and neigh faces
    //     // repeat until next face is end face

    //     return null;
    // }


    public List<BarycentricVector> GetAllIntersectionWithFaces()
    {
        HashSet<BarycentricVector> allIntersections = new HashSet<BarycentricVector>();

        // foreach (Face face in GetTraversedFaces())
        // {

        // }

        return allIntersections.ToList();
    }
}

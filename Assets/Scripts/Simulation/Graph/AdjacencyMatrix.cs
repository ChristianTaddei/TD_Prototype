using System;
using System.Collections.Generic;

public class AdjacencyMatrix<T>
{
    private Dictionary<Tuple<Vertex, Vertex>, T> matrix;

    public bool TryGetValue(Vertex key1, Vertex key2, out T value)
    {
        return matrix.TryGetValue(new Tuple<Vertex, Vertex>(key1, key2), out value);
    }

    public void Add(Vertex key1, Vertex key2, T value){
        matrix.Add(new Tuple<Vertex, Vertex>(key1, key2), value);
    }

    public AdjacencyMatrix(IEnumerable<Vertex> keys, Func<Vertex, Vertex, T> filler)
    {
        matrix = new Dictionary<Tuple<Vertex, Vertex>, T>();
        
        foreach (Vertex vertex in keys)
        {
            foreach (Vertex neighbour in vertex.Neighbours)
            {
                matrix.Add(new Tuple<Vertex, Vertex>(vertex, neighbour), filler.Invoke(vertex, neighbour));
            }
        }
    }
}
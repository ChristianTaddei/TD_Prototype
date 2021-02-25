using System;
using UnityEngine;

public abstract class Representable<T> : IRepresentable where T : IRepresentation
{
    public T Representation { get; set; }

    public abstract string PrefabString { get; }

    IRepresentation IRepresentable.Representation { get => Representation; set => Representation = (T)value; }

    public void MakeRepresentation()
    {
        // throw new NotImplementedException();
    }
}

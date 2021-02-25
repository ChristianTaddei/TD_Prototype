using System;

public interface IRepresentable
{
    IRepresentation Representation { get; set; }

    String PrefabString { get; } // TODO: remove
}

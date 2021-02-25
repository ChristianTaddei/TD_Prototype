using UnityEngine;

public interface IDestructible : IPartialState
{
    bool Destroyed {get; set;}
}

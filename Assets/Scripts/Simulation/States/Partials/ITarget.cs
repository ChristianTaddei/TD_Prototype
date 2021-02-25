using UnityEngine;

public interface ITarget : IPartialState
{
    Vector3 Target { get; set; }
}
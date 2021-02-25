using UnityEngine;

public interface IDirection: IPartialState
{
    Vector3 Direction { get; set; }
}

using UnityEngine;

public abstract class PartialRepresentation<T> : MonoBehaviour where T : IPartialState
{
    public abstract void SetPrevRepresentedState(T value);
    public abstract void SetNextRepresentedState(T value);

    protected abstract void Start();

    private void Update() { Sync(); }

    public abstract void Sync();

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}

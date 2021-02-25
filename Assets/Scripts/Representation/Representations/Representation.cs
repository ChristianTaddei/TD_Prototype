using UnityEngine;

public abstract class Representation<T> : MonoBehaviour, IRepresentation where T : IState
{
    public IRepresentable RepresentedObject { get; set; }

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

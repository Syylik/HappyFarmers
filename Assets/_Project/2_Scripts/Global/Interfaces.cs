using UnityEngine;

public interface ICollectable
{
    public Transform transform { get; }
    public void Collect();
}

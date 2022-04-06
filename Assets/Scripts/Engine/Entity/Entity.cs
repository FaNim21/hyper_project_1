using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected virtual void Awake() { }
    protected abstract void Update();
    protected abstract void FixedUpdate();

    public virtual void TakeDamage(int damage) { }
}

using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [ReadOnly] public float health;
    public int maxHealth;

    protected virtual void Awake()
    {
        health = maxHealth;
    }
    protected abstract void Update();
    protected abstract void FixedUpdate();

    public abstract void TakeDamage(int damage);
}

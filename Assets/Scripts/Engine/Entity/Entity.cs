using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [ReadOnly] public float health;
    public int maxHealth;

    public new Transform transform;
    public Vector2 position;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        health = maxHealth;
    }
    protected virtual void Update()
    {
        position = transform.position;
    }
    protected abstract void FixedUpdate();

    public abstract void TakeDamage(int damage);
}

using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [ReadOnly] public float health;
    public int maxHealth;

    [HideInInspector] public new Transform transform;
    public Vector2 position;

    public virtual void Awake()
    {
        transform = GetComponent<Transform>();
        health = maxHealth;
    }
    public virtual void Update()
    {
        position = transform.position;
    }
    public abstract void FixedUpdate();

    public abstract void TakeDamage(int damage);
}

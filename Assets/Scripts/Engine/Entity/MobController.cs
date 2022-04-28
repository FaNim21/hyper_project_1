using HyperRPG.Engine.Visual;
using UnityEngine;

public class MobController : Entity
{
    [Header("Komponenty")]
    public Rigidbody2D rb;

    [Header("Obiekty")]
    public Transform target;

    [Header("Wartosci")]
    public float chaseRange;
    public float moveSpeed;

    [Header("Debug")]
    [SerializeField, ReadOnly] private Vector2 direction;
    [SerializeField, ReadOnly] private float toTargetAngle;

    protected override void Update()
    {
        direction = (target.position - transform.position).normalized;
        toTargetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //if (health <= 0) Destroy(gameObject);
    }
    protected override void FixedUpdate()
    {
        if(IsTargetInDistance(chaseRange) && !IsTargetInDistance(1f))
            rb.MovePosition((Vector2)rb.position + moveSpeed * Time.deltaTime * direction);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, direction * 3);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public override void TakeDamage(int damage)
    {
        //Narazie jest to bazowa metoda do przyjmowania dmg

        Popup.Create(transform.position, damage.ToString(), Color.red, transform);
        health -= damage;
    }

    public bool IsTargetInDistance(float distance)
    {
        float sqrDistance = (target.position - transform.position).sqrMagnitude;
        return sqrDistance < distance * distance;
    }
}

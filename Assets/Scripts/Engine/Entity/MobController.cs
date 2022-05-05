using HyperRPG.Engine.Visual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : Entity
{
    public static List<MobController> mobs = new();

    [Header("Komponenty")]
    public Rigidbody2D rb;

    [Header("Obiekty")]
    public Transform target;

    [Header("Wartosci")]
    public float chaseRange;
    public float moveSpeed;

    public int damage;
    public float projectileSpeed;
    public float shootingCooldown;

    [Header("Debug")]
    [SerializeField, ReadOnly] private bool isShooting;
    [SerializeField, ReadOnly] private Vector2 direction;
    [SerializeField, ReadOnly] private float toTargetAngle;

    private readonly string _layerMask = "ProjectileMob";

    protected override void Update()
    {
        direction = (target.position - transform.position).normalized;
        toTargetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (IsTargetInDistance(chaseRange) && !isShooting) StartCoroutine(Shooting());

        //if (health <= 0) Destroy(gameObject);
    }
    protected override void FixedUpdate()
    {
        if (IsTargetInDistance(chaseRange) && !IsTargetInDistance(1f))
            rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * direction);
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

    public virtual IEnumerator Shooting()
    {
        isShooting = true;

        var projectile = Instantiate(GameManager.Projectile, transform.position, Quaternion.Euler(0, 0, toTargetAngle));
        projectile.Setup(_layerMask, Quaternion.Euler(0, 0, toTargetAngle) * Vector2.right, projectileSpeed, damage);

        yield return new WaitForSeconds(shootingCooldown);

        isShooting = false;
    }

    public bool IsTargetInDistance(float distance)
    {
        float sqrDistance = (target.position - transform.position).sqrMagnitude;
        return sqrDistance < distance * distance;
    }

    public static MobController GetClosestMob(Transform basePosition)
    {

        return null;
    }
}

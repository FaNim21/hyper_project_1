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

    protected override void Awake()
    {
        base.Awake();

        mobs.Add(this);
    }
    protected override void Update()
    {
        base.Update();

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

    /// <summary>
    /// Unikanie mobów od siebie metod¹ ucieczki od siebie
    /// </summary>
    public Vector2 Avoidance()
    {
        Vector2 avoidVector = Vector2.zero;

        float avoidanceRadius = 0.75f;
        var mobList = GetMobsInRange(position, avoidanceRadius);

        if (mobList.Count == 0 || mobList == null) return avoidVector;

        foreach (var mob in mobList)
            avoidVector += RunAway(mob.position) / 10f;

        return avoidVector.normalized;
    }

    /// <summary>
    /// Oddzielanie mobów od siebie tworz¹c dystans pomiêdzy
    /// </summary>
    public Vector2 Separation()
    {
        Vector2 separateVector = Vector2.zero;

        float separateRadius = 2f;
        var mobList = GetMobsInRange(position, separateRadius);

        if (mobList.Count == 0 || mobList == null) return separateVector;

        foreach (var mob in mobList)
        {
            Vector2 movingTowards = position - mob.position;
            if (movingTowards.magnitude > 0)
                separateVector += movingTowards.normalized / movingTowards.magnitude;
        }
        return separateVector.normalized;
    }

    public Vector2 RunAway(Vector2 target)
    {
        Vector2 neededDirection = (position - target).normalized;
        return neededDirection;
    }

    public static Entity GetClosestMob(Vector2 position, float range)
    {
        if (mobs.Count == 0) return null;

        Entity closestMob = null;

        //TODO: Zoptymalizowac to
        for (int i = 0; i < mobs.Count; i++)
        {
            var currentMob = mobs[i];

            if (Vector2.Distance(position, currentMob.position) > range)
                continue;

            if (closestMob == null)
                closestMob = currentMob;
            else
            {
                if (Vector2.Distance(position, currentMob.position) < Vector2.Distance(position, closestMob.position))
                {
                    closestMob = currentMob;
                }
            }
        }
        return closestMob;
    }
    public static List<Entity> GetMobsInRange(Vector2 position, float range)
    {
        if (mobs.Count == 0) return null;

        List<Entity> inRangeMobs = new();

        //TODO: To tez zoptymalizowac
        for (int i = 0; i < mobs.Count; i++)
        {
            var currentMob = mobs[i];

            if (Vector2.Distance(position, currentMob.position) <= range)
                inRangeMobs.Add(currentMob);
        }

        //TO ROBI GARBAGE COLLECTION ZMIENIC NA TOTALNIE COS INNEGO
        return inRangeMobs;
    }
}

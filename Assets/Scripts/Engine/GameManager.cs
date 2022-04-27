using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public List<Entity> entities = new();

    public static Projectile projectile { get { return GameManager.instance.prefabProjectile; } }
    public Projectile prefabProjectile;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public Entity FindClosestTarget()
    {
        //Nie zrobione
        return entities[0];
    }
}

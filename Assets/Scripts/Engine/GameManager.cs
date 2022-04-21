using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

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
}

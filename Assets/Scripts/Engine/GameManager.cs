using System.Collections.Generic;
using UnityEngine;
using HyperRPG.Engine.Visual;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public List<Entity> entities = new();

    public static Popup Popup { get { return GameManager.instance.prefabPopup; } }
    public Popup prefabPopup;

    public static Projectile Projectile { get { return GameManager.instance.prefabProjectile; } }
    public Projectile prefabProjectile;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Popup.InitializePooling();
    }

    public Entity FindClosestTarget()
    {
        //Nie zrobione(Znajduje sie to tez tymczasowo w GameManagerze lepiej dac to do bazowej klasy moba)
        return entities[0];
    }
}

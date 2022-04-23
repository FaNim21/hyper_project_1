using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;

    public int damage;
    public float speed;

    private Vector2 velocity;

    public void Setup(string layerMask, Vector2 velocity, float speed, int damage)
    {
        gameObject.layer = LayerMask.NameToLayer(layerMask);
        gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer(layerMask);

        this.velocity = velocity;
        this.speed = speed;
        this.damage = damage;

        Destroy(gameObject, 3f);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime * velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.transform.parent.GetComponent<Entity>();
        if (entity != null)
            Debug.Log(entity.name);
    }
}

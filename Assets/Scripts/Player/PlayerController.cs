using UnityEngine;

public class PlayerController : Entity
{
    public static PlayerController instance { get; private set; }

    [Header("Komponenty")]
    [SerializeField] private Rigidbody2D rb;

    [Header("G³ówne wartoœci")]
    public float speed;

    [Header("Debug")]
    [ReadOnly] public Vector2 inputDirection;
    [ReadOnly] public Vector2 mousePosition;
    [ReadOnly] public Vector2 aimDirection;


    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    protected override void Update()
    {
        HandleMovement();

        mousePosition = Utils.GetMouseWorldPosition();
        aimDirection = (mousePosition - (Vector2)transform.position).normalized;
    }
    protected override void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + speed * Time.deltaTime * inputDirection);
    }

    private void HandleMovement()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        inputDirection = inputDirection.normalized;
    }
}

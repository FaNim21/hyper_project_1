using System;
using UnityEngine;

public class PlayerController : Entity
{
    public static PlayerController instance { get; private set; }

    [Header("Komponenty")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Obiekty")]
    public GameObject Phone;
    public GameObject canvas;

    [Header("G³ówne wartoœci")]
    public float speed;
    public float sprintSpeed;

    [Header("Debug")]
    [ReadOnly] public float currentSpeed;
    [ReadOnly] public float aimAngle;
    [ReadOnly] public Vector2 inputDirection;
    [ReadOnly] public Vector2 mousePosition;
    [ReadOnly] public Vector2 aimDirection;

    private readonly float _lerpBetweenSpeed = 2f;
    private string _layerMask = "ProjectilePlayer";


    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        currentSpeed = speed;
    }
    protected override void Update()
    {
        HandleMovement();
        HandleInput();

        mousePosition = Utils.GetMouseWorldPosition();
        aimDirection = (mousePosition - (Vector2)transform.position).normalized;
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButtonDown(0)) Shoot();

        /*int layerMask = ~(LayerMask.GetMask("Player"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDirection, 2f, layerMask);

        if (hit.collider != null)
            Debug.Log(hit.collider.gameObject.name);*/
    }
    protected override void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentSpeed * Time.deltaTime * inputDirection);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, aimDirection * 2);
    }

    private void Shoot()
    {
        var projectile = Instantiate(GameManager.projectile, transform.position, Quaternion.Euler(0, 0, aimAngle));
        projectile.Setup(_layerMask, aimDirection, 4f, 10);
    }

    /// <summary>
    /// Metoda zawieraj¹ca wszystkie odwolania do klawiatury
    /// np. Esc - jako wejscie do menu
    /// </summary>
    private void HandleInput()
    {
        //Na kliknieciu escape aktywujemy i dezaktywujemy Telefon
        if (Input.GetKeyDown(KeyCode.Escape)) Phone.SetActive(!Phone.activeSelf);

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = Mathf.Lerp(currentSpeed, sprintSpeed, _lerpBetweenSpeed * Time.deltaTime);
        else
            currentSpeed = Mathf.Lerp(currentSpeed, speed, _lerpBetweenSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Tymczasowe metoda w Player Controllerze ktora odpowiada za Dezaktywacje i aktywacje main menu z poziomu telefonu
    /// </summary>
    public void OpenCloseMainMenu()
    {
        canvas.SetActive(!canvas.activeSelf);
        Time.timeScale = canvas.activeSelf ? 0 : 1;
    }

    private void HandleMovement()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        inputDirection = inputDirection.normalized;
    }
}

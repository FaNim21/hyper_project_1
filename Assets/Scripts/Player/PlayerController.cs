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
    [ReadOnly] public Vector2 inputDirection;
    [ReadOnly] public Vector2 mousePosition;
    [ReadOnly] public Vector2 aimDirection;

    private readonly float _lerpBetweenSpeed = 2f;


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
    }
    protected override void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentSpeed * Time.deltaTime * inputDirection);
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

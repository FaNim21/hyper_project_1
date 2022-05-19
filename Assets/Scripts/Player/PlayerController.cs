using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using HyperRPG.Engine.Visual;
using TMPro;

public class PlayerController : Entity
{
    public static PlayerController instance { get; private set; }

    [Header("Komponenty")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private CanvasHandle canvasHandle;

    [Header("Obiekty")]
    public GameObject Phone;
    public GameObject canvas;

    [Header("UI")]
    public Image healthFill;
    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI maxHealthValue;

    [Header("G³ówne wartoœci")]
    public float speed;
    public float sprintSpeed;
    public float projectileSpeed;
    public float dashCooldown;
    public float dashForce;

    private float _dashTimer;

    [Header("Debug")]
    [ReadOnly] public bool isInvulnerable;
    [ReadOnly] public bool isDashing;
    [ReadOnly] public float currentSpeed;
    [ReadOnly] public float aimAngle;
    [ReadOnly] public Vector2 inputDirection;
    [ReadOnly] public Vector2 mousePosition;
    [ReadOnly] public Vector2 aimDirection;

    private readonly float _lerpBetweenSpeed = 2f;
    private readonly string _layerMask = "ProjectilePlayer";


    public override void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        base.Awake();

        healthValue.SetText(health.ToString());
        maxHealthValue.SetText(maxHealth.ToString());
        healthFill.fillAmount = health / maxHealth;

        currentSpeed = speed;
        animator.SetFloat("Horizontal", 1);
    }
    public override void Update()
    {
        HandleMovement();
        HandleInput();

        base.Update();

        //Debug.Log(MobController.GetDistanceOfClosestMob(position));

        mousePosition = Utils.GetMouseWorldPosition();
        aimDirection = (mousePosition - (Vector2)transform.position).normalized;
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButtonDown(0) && !canvasHandle.isCanvasEnabled && !EventSystem.current.IsPointerOverGameObject()) Shoot();

        if (health <= 0f) Respawn();
    }

    /// <summary>
    /// Unity event do aktualizawania rzeczy pod FIZYKE
    /// </summary>
    public override void FixedUpdate()
    {
        //logika dasha
        if (isDashing)
        {
            //jezeli timer jest mniejszy od cooldownu to rb.MovePosition(tu cala fizyka)
            //a jezeli timer przekroczy cooldown to zerujemy timer i dajemy is dashing na false
        }

        if (!isDashing)
            rb.MovePosition(rb.position + currentSpeed * Time.deltaTime * inputDirection);
    }

    /// <summary>
    /// Rysujemy linie pomocnicze do obserwowania wszystkich najwazniejszych zachowan i wartosci
    /// </summary>
    private void OnDrawGizmos()
    {
        if (transform == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(position, aimDirection * 2);
    }

    /// <summary>
    /// Metoda zawieraj¹ca wszystkie odwolania do klawiatury
    /// np. Esc - jako wejscie do menu
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = Mathf.Lerp(currentSpeed, sprintSpeed, _lerpBetweenSpeed * Time.deltaTime);
        else
            currentSpeed = Mathf.Lerp(currentSpeed, speed, _lerpBetweenSpeed * Time.deltaTime);

        //klikniemy spacje to uruchamia nam dash
    }

    /// <summary>
    /// Pobieranie i ustalanie najwazniejszych parametrow pod wczytywanie klawiszow wsad i wysylanie wartosci do animacji postaci na movement
    /// </summary>
    private void HandleMovement()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        inputDirection = inputDirection.normalized;

        if (canvasHandle.isCanvasEnabled) return;

        if (inputDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", inputDirection.x);
            animator.SetFloat("Vertical", inputDirection.y);
        }

        animator.SetFloat("Speed", inputDirection.magnitude);
    }

    /// <summary>
    /// Najprostszy sposob strzelania projetile na range jako jako Instatiate projectile prefab ktory znajduje sie w Game managerze i nadawanie mu odrazu wartosci do dzialania
    /// </summary>
    private void Shoot()
    {
        var projectile = Instantiate(GameManager.Projectile, transform.position, Quaternion.Euler(0, 0, aimAngle));
        projectile.Setup(_layerMask, Quaternion.Euler(0, 0, aimAngle) * Vector2.right, projectileSpeed, 10);
    }

    /// <summary>
    /// Postac przyjmuje Damage od projectile wypuszczonego przez moba lub na melee
    /// </summary>
    public override void TakeDamage(int damage)
    {
        if (damage <= 0 || isInvulnerable) return;

        health -= damage;

        Popup.Create(transform.position, damage.ToString(), Color.red, transform);
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        healthFill.fillAmount = health / maxHealth;
        healthValue.SetText(health.ToString());
    }

    public void Respawn()
    {
        health = maxHealth;
    }
}
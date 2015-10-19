using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ITakeDamage
{

    private bool isFacingRight;
    private CharacterController2D controller;
    private float horizontalSpeed;
    private Animator _characterAnimator;


    public float maxSpeed = 8;
    public float SpeedAccelirationOnGround = 10f;
    public float SpeedAccelirationOnAir = 5f;
    public int maxHeath = 100;
    public GameObject damageEffect;
    public SpellFireWall FireSpell;
    public SpellImmobiliseTrap ImmobiliseTrap;
    public Projectile Projectile;
    public float FireRate;
    public Transform ProjectileFireLocation;

    public int Health { get; private set; }
    public bool IsDead { get; set; }

    private float _canFireIn;

    public void Awake()
    {
        IsDead = false;
        controller = GetComponent<CharacterController2D>();
        isFacingRight = transform.localScale.x > 0;
        Health = maxHeath;
    }

    public void Update()
    {

        _canFireIn -= Time.deltaTime;

        if (!IsDead)
            HandleInput();

        var mouvementFactor = controller.State.IsGrounded ? SpeedAccelirationOnGround : SpeedAccelirationOnAir;
        if (IsDead)
            controller.SetHorizontalForce(0);
        else
            controller.SetHorizontalForce(Mathf.Lerp(controller.Velocity.x, horizontalSpeed * maxSpeed, Time.deltaTime * mouvementFactor));

    }

    void Start()
    {
        _characterAnimator = GetComponentInChildren<Animator>();
    }

    public void Kill()
    {
        controller.HandleCollisions = false;
        collider2D.enabled = false;
        IsDead = true;
        controller.SetVerticalForce(10);
        Health = 0;
    }

    public void TakeDamage(int damage)
    {
        FloatingText.Show(string.Format("-{0}!", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
        Instantiate(damageEffect, transform.position, transform.rotation);
        Health -= damage;
        if (Health <= 0)
        {
            LevelManager.Instance.KillPlayer();
        }
    }

    public void RespawnAt(Transform spawnPoint)
    {
        Health = maxHeath;
        if (!isFacingRight)
            flip();
        IsDead = false;
        collider2D.enabled = true;
        controller.HandleCollisions = true;

        transform.position = spawnPoint.position;
    }

    private void UseFireWallSpell()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var raycast = Physics2D.Raycast(new Vector2(mousePosition.x,mousePosition.y), -Vector2.up, 100, controller.PlatformMask);
        if (raycast)
        {
            var UsedSPell = (SpellFireWall)Instantiate(FireSpell, raycast.point, transform.rotation);
            UsedSPell.Initialize(gameObject, 20);
        }
    }

    private void UseImmobilizeTrapSpell()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var raycast = Physics2D.Raycast(new Vector2(mousePosition.x, mousePosition.y), -Vector2.up, 100, controller.PlatformMask);
        if (raycast)
        {
            var UsedSPell = (SpellImmobiliseTrap)Instantiate(ImmobiliseTrap, raycast.point, transform.rotation);
            UsedSPell.Initialize(gameObject, 20);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UseFireWallSpell();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseImmobilizeTrapSpell();
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalSpeed = 1;
            if (!isFacingRight)
                flip();
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            horizontalSpeed = -1;
            if (isFacingRight)
                flip();

        }
        else
        {
            horizontalSpeed = 0;

        }

        if (controller.CanJump && Input.GetButtonDown("Jump"))
        {
            controller.Jump();
            _characterAnimator.SetTrigger("jump");
        }
        if (Input.GetMouseButtonDown(0))
            FireProjectile();
        if (controller.CanJump)
        {
            if (horizontalSpeed == 0)
                _characterAnimator.SetTrigger("idle");
            else
            {
                _characterAnimator.SetTrigger("run");
            }
        }
        else
            _characterAnimator.SetTrigger("jump");
    }

    private void FireProjectile()
    {
        if (_canFireIn > 0)
            return;
        //var direction = isFacingRight ? Vector2.right : -Vector2.right;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = (mousePosition - ProjectileFireLocation.position);
        var projectile = (Projectile)Instantiate(Projectile, ProjectileFireLocation.transform.position, ProjectileFireLocation.transform.rotation);
        projectile.Initialize(gameObject, new Vector3(direction.x, direction.y , 0).normalized, controller.Velocity);
        _canFireIn = FireRate;
    }

    private void flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = transform.localScale.x > 0;
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        FloatingText.Show(string.Format("-{0}!", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
        Instantiate(damageEffect, transform.position, transform.rotation);
        Health -= damage;
        if (Health <= 0)
        {
            LevelManager.Instance.KillPlayer();
        }
    }

    public void addHealth(int health)
    {
        FloatingText.Show(string.Format("+{0}!", health), "PlayerAddHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
        Health = Mathf.Min(Health + health, maxHeath);
    }


    public void TakePeriodicDamage(int duration, int damage, GameObject instigator)
    {
        throw new System.NotImplementedException();
    }
}

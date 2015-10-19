using UnityEngine;
using System.Collections;

public class SimpleEnnemyAI : MonoBehaviour, ITakeDamage,ITakeEffect
{
    public float Speed;
    public float FireRate = 1;
    public Projectile Projectile;
    public GameObject DestroyedEffect;
    public int PointsToGiveToPlyer = 50;
    public int MaxHealth = 100;
    public float range=10f;

    private CharacterController2D _controller;
    private Vector2 _direction;
    private Vector2 _startPosition;
    private float _canFireIn;
    private int _currentHealth;
    private float _slowValue;

    // Use this for initialization
    void Start()
    {
        _currentHealth = MaxHealth;
        _slowValue = 1;
        _controller = GetComponent<CharacterController2D>();
        if (transform.localScale.x > 0)
            _direction = new Vector2(-1, 0);
        else
            _direction = new Vector2(1, 0);
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

            _controller.SetHorizontalForce(_direction.x * Speed*_slowValue);
            if ((_direction.x < 0 && _controller.State.IsCollindingLeft) || (_direction.x > 0 && _controller.State.IsCollindingRight))
            {
                _direction = -_direction;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
       
        if ((_canFireIn -= Time.deltaTime) > 0)
        {
            return;
        }
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 3, 1 << LayerMask.NameToLayer("Projectiles"));
        /*Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x+(_direction.x*10), transform.position.y+5), 1 << LayerMask.NameToLayer("Projectiles"));
        foreach(Collider2D c in colliders)
        {
            var playerProjectile = c.gameObject.GetComponent<NormalProjectile>();
            if(playerProjectile!=null && playerProjectile.Owner.GetComponent<Player>()!=null)
            {
                var playerProjectilePosition = playerProjectile.gameObject.transform.position;
                var myProjectileDirection = playerProjectilePosition - transform.position;
                myProjectileDirection = new Vector3(myProjectileDirection.x, myProjectileDirection.y, 0).normalized;
                var counterProjectile = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
                counterProjectile.Initialize(gameObject, myProjectileDirection, _controller.Velocity);
                _canFireIn = FireRate;
                return;
            }
        }*/
        /*colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y-1.26f), new Vector2(transform.position.x + (_direction.x * 5), transform.position.y + 5), 1 << LayerMask.NameToLayer("Spells"));
        foreach (Collider2D c in colliders)
        {
            var spell =(Spells) c.gameObject.GetComponent(typeof(Spells));
            if (spell != null && spell.Owner.GetComponent<Player>() != null && _controller.CanJump)
            {
               _controller.Jump();
               _controller.SetVerticalForce(_controller.Velocity.y * _slowValue);
            }
        }*/
        var raycast = Physics2D.Raycast(transform.position, _direction, 10, 1 << LayerMask.NameToLayer("Player"));
        if (!raycast)
            return;
        var projectile = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
        projectile.Initialize(gameObject, _direction, _controller.Velocity);
        _canFireIn = FireRate;
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        if (PointsToGiveToPlyer != 0)
        {
            var projectile = instigator.GetComponent<Projectile>();
            if (projectile != null && projectile.Owner.GetComponent<Player>() != null)
            {
                _currentHealth -= damage;
                FloatingText.Show(string.Format("-{0}!", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
            }
            var spell = instigator.GetComponent<Spells>();
            if (spell != null && spell.Owner.GetComponent<Player>() != null)
            {
                _currentHealth -= damage;
                FloatingText.Show(string.Format("-{0}!", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
            }
        }
        if (_currentHealth <= 0)
        {
            DestroyEnnemie();
        }
    }

    public void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player)
    {
        _direction = new Vector2(-1, 0);
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = _startPosition;
        _currentHealth = MaxHealth;
        gameObject.SetActive(true);
    }


    public void TakePeriodicDamage(int duration, int damage, GameObject instigator)
    {
        StartCoroutine(BurnDamage(duration, damage));
    }
    private IEnumerator BurnDamage(int duration, int damage)
    {
        for (int i = 0; i < duration; i++)
        {
            _currentHealth -= damage;
            FloatingText.Show(string.Format("-{0}!", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
            if (_currentHealth <= 0)
            {
                DestroyEnnemie();
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private void DestroyEnnemie()
    {
        if (LevelThreeTimer.Instance != null)
            LevelThreeTimer.Instance.RemoveEnnemie();
        GameManager.Instance.AddPoints(PointsToGiveToPlyer);
        FloatingText.Show(string.Format("+{0}!", PointsToGiveToPlyer), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
        Instantiate(DestroyedEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    public void Slow(int duration,float pourcentage)
    {
        _slowValue = Mathf.Max(1 - pourcentage, 0);
        StartCoroutine(SlowCo(duration));
    }
    private IEnumerator SlowCo(int duration)
    {
        yield return new WaitForSeconds(duration);
        _slowValue = 1;
    }
}

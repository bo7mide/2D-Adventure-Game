using UnityEngine;
using System.Collections;

public class PathedProjectile : Projectile,ITakeDamage
{

    private Transform _destination;
    private float _speed;
    public int PointsToGivePlyer=0;
    public int Damage = 10;
    public GameObject DestroyedEffect;

    public void Initialize(Transform Destination, float speed)
    {
        _destination = Destination;
        _speed = speed;
    }

    
    public void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, _destination.position, _speed * Time.deltaTime);
        var distanceSquared = (_destination.position - transform.position).sqrMagnitude;
        if (distanceSquared > 0.1f * 0.1f)
            return;
        Destroy(gameObject);
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        var projectile = instigator.GetComponent<Projectile>();
        if (projectile != null && projectile.Owner.GetComponent<Player>() != null && PointsToGivePlyer != 0)
        {
            GameManager.Instance.AddPoints(PointsToGivePlyer);
            FloatingText.Show(string.Format("+{0}!", PointsToGivePlyer), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
        }
        Destroy(gameObject);
    }

    protected override void OnCollideOther(Collider2D other)
    {
        DestroyProjectile();
    }
    protected override void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {
        //takeDamage.TakeDamage(Damage, gameObject);
        DestroyProjectile();
    }
    private void DestroyProjectile()
    {
        if (DestroyedEffect != null)
            Instantiate(DestroyedEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    public void TakePeriodicDamage(int duration, int damage, GameObject instigator)
    {
        throw new System.NotImplementedException();
    }
}

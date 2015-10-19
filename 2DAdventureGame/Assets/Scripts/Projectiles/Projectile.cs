using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {

    public float Speed;
    public LayerMask CollisionMask;

    public GameObject Owner { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 InitialVelocity { get; private set; }

    public void Initialize(GameObject owner,Vector2 direction,Vector2 initialVelocity)
    {
        transform.right = direction;
        Owner = owner;
        Direction = direction;
        InitialVelocity = initialVelocity;
        OnInitialized();
    }

    protected virtual void OnInitialized()
    { 
        
    }

    // Layer n  = Binary     Decimal
    // Layer 0  = 0000 0001 =1
    // Layer 7  = 1000 0000 =128
    //GameObject.layer = n;
    //LayerMask.value = Somme binary
    //Layer%ask.value(Layer 5,2)= 0010 0100

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if ((CollisionMask.value & (1 << other.gameObject.layer)) == 0)
        {
            OnNotCollideWith(other);
            return;
        }
        var isOwner = other.gameObject == Owner;
        if(isOwner)
        {
            OnCollideOwner();
            return;
        }
        var takeDamage = (ITakeDamage)other.GetComponent(typeof(ITakeDamage));
        if (takeDamage!=null)
        {
            OnCollideTakeDamage(other, takeDamage);
            return;
        }
        OnCollideOther(other);
    }

    protected virtual void OnNotCollideWith(Collider2D other)
    {
        
    }

    protected virtual void OnCollideOwner()
    {

    }
    protected virtual void OnCollideTakeDamage(Collider2D other,ITakeDamage takeDamage)
    {

    }
    protected virtual void OnCollideOther(Collider2D other)
    {

    }
}

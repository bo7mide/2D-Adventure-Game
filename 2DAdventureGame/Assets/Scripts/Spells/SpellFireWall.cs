using UnityEngine;
using System.Collections;

public class SpellFireWall : Spells {

    void Start()
    {
        Destroy(gameObject, 3f);
    }
	// Use this for initialization
    protected override void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {
        takeDamage.TakePeriodicDamage(5,Damage, gameObject);
    }

}

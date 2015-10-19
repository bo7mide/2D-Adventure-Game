using UnityEngine;
using System.Collections;

public class SpellImmobiliseTrap : Spells {
    public int effectDuration=2;
    [Range(0,1)]
    public float slowPourcentage = 1;

    void Start()
    {
        Destroy(gameObject,2);
    }

    protected override void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {
        takeDamage.TakeDamage(Damage, gameObject);
    }
    protected override void OnCollideTakeEffect(Collider2D other, ITakeEffect takeEffect)
    {
        takeEffect.Slow(effectDuration, slowPourcentage);
    }
}

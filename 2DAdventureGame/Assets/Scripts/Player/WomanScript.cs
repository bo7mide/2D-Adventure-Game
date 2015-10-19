using UnityEngine;
using System.Collections;

public class WomanScript : MonoBehaviour,ITakeDamage {

    public int Life=200;
    public GameObject DestroyedEffect;
    public GameObject HealthBar;
    public int Health { get; private set; }
	// Use this for initialization
	void Start () {
        Health = Life;
	}
	
    public void AddHealth(int healthToAdd)
    {
        FloatingText.Show(string.Format("+{0}!", healthToAdd), "PlayerAddHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
        Health = Mathf.Min(Health + healthToAdd, Life);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int damage, GameObject instigator)
    {
        FloatingText.Show(string.Format("-{0}!", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
        Health -= damage;
        if (Health <= 0)
        {
            if(DestroyedEffect!=null)
                Instantiate(DestroyedEffect, transform.position, transform.rotation);
            LevelManager.Instance.KillPlayer();
            Destroy(HealthBar);
            renderer.active = false;
        }
    }

    public void TakeDamage(int damage)
    {
        FloatingText.Show(string.Format("-{0}!", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
        Health -= damage;
        if (Health <= 0)
        {
            if (DestroyedEffect != null)
                Instantiate(DestroyedEffect, transform.position, transform.rotation);
            LevelManager.Instance.KillPlayer();
            Destroy(HealthBar);
            renderer.active = false;
        }
    }

    public void TakePeriodicDamage(int duration, int damage, GameObject instigator)
    {
        
    }
}

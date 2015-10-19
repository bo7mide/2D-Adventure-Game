using UnityEngine;
using System.Collections;

public class BatScript : MonoBehaviour,ITakeDamage {
    public GameObject Explosion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int damage, GameObject instigator)
    {
        if (Explosion != null)
            Instantiate(Explosion, transform.position, transform.rotation);
        if (LevelThreeTimer.Instance != null)
            LevelThreeTimer.Instance.RemoveEnnemie();
        Destroy(gameObject);
    }

    public void TakePeriodicDamage(int duration, int damage, GameObject instigator)
    {
        
    }
}

using UnityEngine;
using System.Collections;

public class GiveDamageToWoman : MonoBehaviour
{
    public int damageToGive = 10;
    public GameObject ExplotionEffect;
    private Vector2 _lastPosition, _velocity;




    public void OnTriggerEnter2D(Collider2D other)
    {
        var woman = other.GetComponent<WomanScript>();
        if (woman == null)
            return;
        Instantiate(ExplotionEffect, transform.position, transform.rotation);
        woman.TakeDamage(damageToGive);
        if (LevelThreeTimer.Instance != null)
            LevelThreeTimer.Instance.RemoveEnnemie();
        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
        else
            Destroy(gameObject);

    }
}

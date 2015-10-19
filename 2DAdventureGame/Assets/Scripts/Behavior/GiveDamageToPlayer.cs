using UnityEngine;
using System.Collections;

public class GiveDamageToPlayer : MonoBehaviour {
    public int damageToGive = 10;
    public bool ExploseOnContact=false;
    private Vector2 _lastPosition,_velocity;
    public GameObject Explosion;
	
	
	void LateUpdate () {
        _velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;
        _lastPosition = transform.position;
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        player.TakeDamage(damageToGive);
        var controller = player.GetComponent<CharacterController2D>();
        var totalVelocity = controller.Velocity + _velocity;
        controller.SetForce(new Vector2(-1*Mathf.Sign(totalVelocity.x)*Mathf.Clamp(Mathf.Abs(totalVelocity.x)*5,10,20),
            -1 * Mathf.Sign(totalVelocity.y) * Mathf.Clamp(Mathf.Abs(totalVelocity.y) * 3, 5, 10)));
        if (ExploseOnContact)
        {
            if (Explosion != null)
                Instantiate(Explosion, transform.position, transform.rotation);
            if (LevelThreeTimer.Instance != null)
                LevelThreeTimer.Instance.RemoveEnnemie();
            Destroy(gameObject);
        }
    }
}

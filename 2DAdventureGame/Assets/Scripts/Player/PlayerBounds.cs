using UnityEngine;
using System.Collections;

public class PlayerBounds : MonoBehaviour {

    public enum BoundBehavior { 
        Nothing,
        Contains,
        Kill
    }

    public BoxCollider2D Bounds;
    public BoundBehavior Left, Right, Above, Below;

    private Player _player;
    private BoxCollider2D _boxCollider;

	// Use this for initialization
	void Start () {
        _player = GetComponent<Player>();
        _boxCollider = GetComponent<BoxCollider2D>();
	}

    void ApplyBoundsBehavior(BoundBehavior behavior, Vector2 constraintPosition)
    {
        if (behavior == BoundBehavior.Kill)
        {
            LevelManager.Instance.KillPlayer();
            return;
        }
        transform.position = constraintPosition;
    }
	
	// Update is called once per frame
	void Update () {
        if (_player.IsDead)
            return;
        var colliderSize = new Vector2(_boxCollider.size.x * Mathf.Abs(transform.localScale.x), _boxCollider.size.y * Mathf.Abs(transform.localScale.y))/2;
        if (Above!=BoundBehavior.Nothing && transform.position.y + colliderSize.y > Bounds.bounds.max.y)
            ApplyBoundsBehavior(Above, new Vector2(transform.position.x, Bounds.bounds.max.y - colliderSize.y));

        if (Below!= BoundBehavior.Nothing && transform.position.y - colliderSize.y < Bounds.bounds.min.y)
            ApplyBoundsBehavior(Below, new Vector2(transform.position.x, Bounds.bounds.min.y + colliderSize.y));

        if (Right!= BoundBehavior.Nothing && transform.position.x + colliderSize.x > Bounds.bounds.max.x)
            ApplyBoundsBehavior(Right, new Vector2(Bounds.bounds.max.x - colliderSize.x, transform.position.y));

        if (Left!= BoundBehavior.Nothing && transform.position.x - colliderSize.x < Bounds.bounds.min.x)
            ApplyBoundsBehavior(Left, new Vector2(Bounds.bounds.min.x + colliderSize.x, transform.position.y));
	
    }
}

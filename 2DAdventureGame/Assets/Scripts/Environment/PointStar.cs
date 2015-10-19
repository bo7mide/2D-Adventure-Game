using UnityEngine;
using System.Collections;

public class PointStar : MonoBehaviour,IPlayerRespawnListner {

    public GameObject Effect;
    public int PointsToAdd = 10;

    private Animator _animator;
    private bool _isCollected;
    private SpriteRenderer _sprite;

    void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_isCollected)
            return;
        if(other.GetComponent<Player>()==null)
            return;
        GameManager.Instance.AddPoints(PointsToAdd);
        Instantiate(Effect,transform.position,transform.rotation);
        _isCollected = true;
        _animator.SetTrigger("Collect");

        FloatingText.Show(string.Format("+{0}!", PointsToAdd), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
    }

    public void FinishAnimationEvent()
    {
        _animator.SetTrigger("Reset");
        _sprite.enabled = false;
    }

    public void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player)
    {
        _isCollected = false;
        _sprite.enabled = true;
    }
}

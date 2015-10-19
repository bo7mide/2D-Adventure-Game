using UnityEngine;
using System.Collections;

public class EnnemyWizard : MonoBehaviour
{
    public Transform ProjectileSpawner;
    public float coolDown = 5;
    private float _coolDown;
    public Projectile WizardProjectile;
    private Transform Destination;

    public GameObject Explosion;
    // Use this for initialization
    void Start()
    {
        _coolDown = coolDown;
        Destination = FindObjectOfType<WomanScript>().gameObject.transform;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
         Player player=collider.gameObject.GetComponent<Player>();
        if(player!=null)
        {
            if (LevelThreeTimer.Instance != null)
                LevelThreeTimer.Instance.RemoveEnnemie();
            if (Explosion != null)
                Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if ((_coolDown-=Time.deltaTime) > 0)
            return;
        else
        {
            var direction = (ProjectileSpawner.position - Destination.position);
                direction = -direction;
            var projectile = (Projectile)Instantiate(WizardProjectile, ProjectileSpawner.transform.position, ProjectileSpawner.transform.rotation);
            projectile.Initialize(gameObject, new Vector3(direction.x, direction.y, 0).normalized, Vector2.zero);
            _coolDown = coolDown;
        }
    }

}

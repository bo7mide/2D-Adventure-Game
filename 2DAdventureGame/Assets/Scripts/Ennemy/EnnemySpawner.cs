using UnityEngine;
using System.Collections;

public class EnnemySpawner : MonoBehaviour
{

    public float CoolDown;
    public GameObject Ennemy;
    public GameObject SpecialEnnemy;
    public float StartTime;

    
    private float _currentTime;

    // Use this for initialization
    void Start()
    {
        _currentTime = StartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_currentTime -= Time.deltaTime) > 0)
            return;
        else
        {
            if(Ennemy.GetComponent<HealthPack>()==null)
            {
                if (LevelThreeTimer.Instance != null)
                    LevelThreeTimer.Instance.AddEnnemie();
            }
            if (SpecialEnnemy == null)
            {
                Instantiate(Ennemy, transform.position, transform.rotation);
                _currentTime = CoolDown;
            }
            else
            {
                int val;
                val = Random.Range(0, 3);
              if(val==0)
                  Instantiate(SpecialEnnemy, transform.position, transform.rotation);
              else
                  Instantiate(Ennemy, transform.position, transform.rotation);
            }
            _currentTime = CoolDown;
        }
    }
}

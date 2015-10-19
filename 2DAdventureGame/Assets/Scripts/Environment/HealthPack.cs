using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour, IPlayerRespawnListner
{
    public GameObject Effect;
    public int HealthToRestore = 25;

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        var player = (Player)other.gameObject.GetComponent<Player>();
        if (player == null)
            return;
        WomanScript woman = FindObjectOfType<WomanScript>();
        if(woman!=null)
        woman.AddHealth(HealthToRestore);
        player.addHealth(HealthToRestore);
        if(Effect!=null)
        Instantiate(Effect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    public void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player)
    {
        gameObject.SetActive(true);
    }
}



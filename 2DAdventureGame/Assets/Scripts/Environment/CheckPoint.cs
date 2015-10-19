using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPoint : MonoBehaviour
{
    private List<IPlayerRespawnListner> _listeners;
    void Awake()
    {
        _listeners = new List<IPlayerRespawnListner>();
    }
    public void PlayerHitCheckPoint()
    {
        StartCoroutine(PlayerHitCheckPointCo(LevelManager.Instance.CurrentTimeBonus));
    }

    public IEnumerator PlayerHitCheckPointCo(int bonus)
    {
        FloatingText.Show("CheckPoint!","CheckPointText",new CenteredTextPositioner(.5f));
        yield return new WaitForSeconds(.5f);
        FloatingText.Show(string.Format("+{0}! time bonus",bonus), "CheckPointText", new CenteredTextPositioner(.5f));
        
    }

    public void PlayerLeftCheckPoint()
    {

    }

    public void SpawnPlayer(Player player)
    {
        player.RespawnAt(transform);
        foreach(var listner in _listeners)
        {
            listner.OnPlayerRespawnInThisCheckpoint(this, player);
        }
    }

    public void AssignObjectToCheckPoint(IPlayerRespawnListner listener)
    {
        _listeners.Add(listener);
    }

}

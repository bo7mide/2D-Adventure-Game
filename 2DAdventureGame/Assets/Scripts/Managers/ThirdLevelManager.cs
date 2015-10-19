using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThirdLevelManager : MonoBehaviour
{

    public static ThirdLevelManager Instance { get; private set; }
    public Player Player { get; private set; }
    public CameraController Camera { get; private set; }
    public TimeSpan RunningTime { get { return DateTime.UtcNow - _started; } }
    public int CurrentTimeBonus
    {
        get
        {
            var secondDifference = (int)(BonusCutoffSeconds - RunningTime.TotalSeconds);
            if (BonusCutoffSeconds > RunningTime.TotalSeconds)
                secondDifference++;
            return Mathf.Max(0, secondDifference * BonusSecondMultiplier);
        }
    }

    private List<CheckPoint> _checkpoints;
    private int _currentCheckpointIndex;
    private DateTime _started;
    private int _savedPoints;

    public CheckPoint DebugSpawn;
    public int BonusCutoffSeconds, BonusSecondMultiplier;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        _checkpoints = FindObjectsOfType<CheckPoint>().OrderBy(t => t.transform.position.x).ToList();
        _currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

        Player = FindObjectOfType<Player>();
        Camera = FindObjectOfType<CameraController>();

        _started = DateTime.UtcNow;

#if UNITY_EDITOR
        if (DebugSpawn != null)
        {
            DebugSpawn.SpawnPlayer(Player);
        }
        else
        {
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
        }
#else
        if(_currentCheckpointIndex!=-1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
#endif
        var listners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerRespawnListner>();
        foreach (var listner in listners)
        {
            for (var i = _checkpoints.Count - 1; i >= 0; i--)
            {
                var distance = ((MonoBehaviour)listner).transform.position.x - _checkpoints[i].transform.position.x;
                if (distance < 0)
                    continue;
                _checkpoints[i].AssignObjectToCheckPoint(listner);
                break;
            }
        }
    }

    public void Update()
    {
        var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;
        if (isAtLastCheckpoint)
            return;
        var distanceToNextCheckpoint = _checkpoints[_currentCheckpointIndex + 1].transform.position.x - Player.transform.position.x;
        if (distanceToNextCheckpoint >= 0)
            return;
        _checkpoints[_currentCheckpointIndex].PlayerLeftCheckPoint();
        _currentCheckpointIndex++;
        _checkpoints[_currentCheckpointIndex].PlayerHitCheckPoint();

        //Time Bonus
        GameManager.Instance.AddPoints(CurrentTimeBonus);
        _savedPoints = GameManager.Instance.Points;
        _started = DateTime.UtcNow;


    }

    public void KillPlayer()
    {
        StartCoroutine(KillPlayerCo());
    }

    private IEnumerator KillPlayerCo()
    {
        Player.Kill();
        Camera.IsFollowing = false;
        yield return new WaitForSeconds(2f);

        Camera.IsFollowing = true;
        if (_currentCheckpointIndex != -1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);

        //POINTS
        _started = DateTime.UtcNow;
        GameManager.Instance.ResetPoints(_savedPoints);
    }
}

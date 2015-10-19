using UnityEngine;
using System.Collections;

public class LevelThreeTimer : MonoBehaviour
{

    public int timeToSurvive;
    public static LevelThreeTimer Instance { get; private set; }
    private int _remainingTime;
    private float _currentTime;
    private int TotalEnnemies=0;
    private EnnemySpawner[] spawners;
    public GUISkin Skin;
    public void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        spawners = FindObjectsOfType<EnnemySpawner>();
        _remainingTime = timeToSurvive;
        _currentTime = timeToSurvive;
    }

    // Update is called once per frame
    void Update()
    {
        if (_remainingTime > 0)
        {
            _currentTime -= Time.deltaTime;
            _remainingTime = (int)_currentTime;
            return;
        }
        else
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].gameObject.SetActive(false);
            }
            if (TotalEnnemies <= 0)
                LevelManager.Instance.FinishLevel();
        }
    }

    public void AddEnnemie()
    {
        TotalEnnemies++;
        Debug.Log(TotalEnnemies);
    }

    public void RemoveEnnemie()
    {
        TotalEnnemies--;
        Debug.Log(TotalEnnemies);
    }

    public void OnGUI()
    {
        GUI.skin = Skin;
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        {
            GUILayout.BeginVertical(Skin.GetStyle("GameHud"));
            {
                GUILayout.Label(string.Format("Remaining Time:{00}", _remainingTime, LevelManager.Instance.CurrentTimeBonus), Skin.GetStyle("TimeText"));
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();
    }
}

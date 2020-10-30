using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void UpdateScoreEventHandler(Game gameData);
    public delegate void GameControllerStateEventHandler();

    public event UpdateScoreEventHandler UpdateScore;
    public event GameControllerStateEventHandler GameOver;

    public int CurrentLevel;

    public List<LevelData> Levels;
    public Game GameData = new Game();

    private SpawnWavesController _waveSpawner;
    private LevelData _currentLevelDataContaiter;
    private IMob _crystalIMob;

    void Awake()
    {
        _waveSpawner = FindObjectOfType<SpawnWavesController>();
        //_currentLevelDataContaiter = Instantiate(Levels[GameData.Location]);

        //lazy finding crystal
        var mobControllers = FindObjectsOfType<MobStatsController>();

        foreach (var val in mobControllers)
            if (val.GetFraction.name == "Crystal")
            {
                _crystalIMob = val.gameObject.GetComponent<IMob>();
                _crystalIMob.Dead += CrystalIsDestroyed;
                break;
            }
    }

    void Start()
    {
        UpdateScore?.Invoke(GameData);

        _waveSpawner.WavesToSpawn = _currentLevelDataContaiter.LevelWaves.SpawnWaves;
        _waveSpawner.LockSpawn = false;
    }

    private void CrystalIsDestroyed()
    {
        GameOver?.Invoke();
    }
}
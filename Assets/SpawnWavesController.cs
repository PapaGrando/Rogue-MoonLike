using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWavesController : MonoBehaviour
{
    public delegate void SpawnWavesEventHandler();
    public event SpawnWavesEventHandler WaveComplete;
    public event SpawnWavesEventHandler AllWavesComplete;

    /// <summary>
    /// Блокирует спавн мобов. Выставляется true, если  WavesToSpawn пустой
    /// </summary>
    public bool LockSpawn = true;
    public int Alive { get; private set; } = 0;

    public GameObject MobTarget;

    /// <summary>
    /// Волны, которые будут спавниться на уровне.Отсчет с конца. Волна удаляется, если пройдена
    /// </summary>
    public List<Wave> WavesToSpawn;

    private EnemySpawner _leftSpawner;
    private EnemySpawner _rightSpawner;
    private bool _firstSpawn = true;
    private bool _spawning = false;

    void Awake()
    {
        _rightSpawner = FindObjectOfType<RightSpawner>();
        _leftSpawner = FindObjectOfType<LeftSpawner>();
    }

    void Update()
    {
        if (LockSpawn) return;

        if (Alive < 1 && !_spawning)
        { 
            if (WavesToSpawn.Count <= 0)
            {
                AllWavesComplete?.Invoke();
                LockSpawn = true;
                return;
            }

            if (!_firstSpawn)
            {
                WavesToSpawn.RemoveAt(WavesToSpawn.Count - 1);
                WaveComplete?.Invoke();
            }

            
            StartCoroutine(SpawnWave(WavesToSpawn[WavesToSpawn.Count - 1]));
            _firstSpawn = false;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        var enemyCount = wave.Enemies.Count;
        if (enemyCount > 0)
        {
            _spawning = true;

            for (int i = 0; i < enemyCount - 1; i++)
            {
                GameObject spawned = null;

                if(wave.Direction == Direction.Left)
                    spawned = _leftSpawner.SpawnObject(wave.Enemies[i]);
                else if (wave.Direction == Direction.Right)
                    spawned = _rightSpawner.SpawnObject(wave.Enemies[i]);
                else
                    Debug.LogError($"{this.GetType().Name} : incorrect direction. Object skipped");

                if (spawned != null)
                {
                    Alive += 1;
                    spawned.GetComponent<IMob>().Dead += SpawnWavesController_Dead;
                    if (MobTarget != null) spawned.GetComponent<MobAI>().SetTarget(MobTarget);
                }

                yield return new WaitForSeconds(wave.SpawnDelay);
            }

            _spawning = false;
        }
    }

    private void SpawnWavesController_Dead() => Alive -= 1;
}

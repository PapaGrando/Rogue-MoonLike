using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New LevelWaves", menuName = "LevelWaves", order = 52)]
public class LevelWaves : ScriptableObject
{
    /// <summary>
    /// Брать этот список из Start()
    /// </summary>
    [SerializeField] public List<Wave> SpawnWaves;

    [Header("Если RandomizeWaves true, то SpawnWaves будет изменен!")]
    [Space] [Space] [SerializeField] public bool RandomizeWaves;

    [SerializeField] public uint WavesAmount;
    [SerializeField] public uint MinObjectsInWave = 1;
    [SerializeField] public uint MaxObjectsInWave = 5;

    [SerializeField] public List<GameObject> ObjectsToSpawn;

    void Awake()
    {
        if (RandomizeWaves) GenerateWaves();
    }

    void GenerateWaves()
    {
        if (WavesAmount == 0)
        {
            Debug.LogError("Waves Amount is 0");
            return;
        }
        if (ObjectsToSpawn.Count == 0)
        {
            Debug.LogError("No objects to spawn");
            return;
        }

        if (MaxObjectsInWave == 0 || MinObjectsInWave == 0 || MaxObjectsInWave < MinObjectsInWave)
        {
            Debug.LogError("Check Max - Min values of objects in ScriptableObject");
            return;
        }

        //Generating...
        for (int i = 0; i < WavesAmount; i++)
        {
            var newWave = (Wave)CreateInstance(typeof(Wave));

            newWave.Direction = Random.Range(0, 2) == 0 ? Direction.Left : Direction.Right;
            newWave.SpawnDelay = Random.Range(0.5f, 3);

            var enemiesInWave = Random.Range(MinObjectsInWave, MaxObjectsInWave);

            for (int j = 0; j < enemiesInWave; j++)
            {
                newWave.Enemies.Add(ObjectsToSpawn[Random.Range(0,ObjectsToSpawn.Count)]);   
            }

            SpawnWaves.Add(newWave);
        }
    }
}
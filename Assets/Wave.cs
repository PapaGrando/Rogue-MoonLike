using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave", order = 51)]
public class Wave : ScriptableObject
{
    /// <summary>
    /// Какие враги будут спавниться и сторона спавнера
    /// </summary>
    public List<GameObject> Enemies = new List<GameObject>();
    public float SpawnDelay = 2f;
    public Direction Direction;
}
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public int Score;
    public int Location;
    public int Wave;

    public bool GameOver;

    public List<GameObject> EnemiesInScene = new List<GameObject>();
}

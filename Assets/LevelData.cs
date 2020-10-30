using System.Collections.Generic;
using UnityEngine;

//todo : дополнить. (сделать CustomEditor?)
[CreateAssetMenu(fileName = "LevelData_X", menuName = "LevelData", order = 51)]
public class LevelData : ScriptableObject
{
    public LevelWaves LevelWaves;

    [Header("Спрайты для бэкгроунда. Первый - дальний. Макс - 5")]
    
    //В инспекторе без CustomEditor нельзя ограничить размер массива :/ 
    public Sprite[] BackgroundsSprites = new Sprite[5];
}

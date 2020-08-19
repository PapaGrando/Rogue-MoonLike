using UnityEngine;

[CreateAssetMenu(fileName = "New BaffStats", menuName = "Baff Stats", order = 51)]
public class BuffStats : ScriptableObject
{
    //todo: реализовать поля ниже и переписать конструкторы
    public string Name;
    public Sprite Icon;
    public string Description;

    public MobStats MobStats;

    [Space] [Tooltip("Если true, ActionTime может суммироваться при активации нескольких одинаковых баффов одновременно")]
    public bool ActionTimeCanSum = false;
    [Range(0, float.PositiveInfinity)] public float ActionTime;

    public BuffStats(MobStats stats, float time)
    {
        MobStats = stats;
        ActionTime = time;
    }

    /// <summary>
    /// Баф с без времени - бесконечный
    /// </summary>
    public BuffStats(MobStats stats)
    {
        MobStats = stats;
        ActionTime = float.PositiveInfinity;
    }
}

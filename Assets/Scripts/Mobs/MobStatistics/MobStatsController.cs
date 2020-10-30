using UnityEngine;
using System.Collections.Generic;

//todo : переделать логику
public class MobStatsController : MonoBehaviour
{
    //События
    public delegate void BuffDictionaryEventHandler(Dictionary<BuffStats, float> buffStats);
    public event BuffDictionaryEventHandler BuffsUpdated;

    public delegate void MobStatsEventHandler(MobStats mobStats);
    public event MobStatsEventHandler MobStatsUpdated;

    public MobStats GetStats => _mobStats + _bonusesStats;
    public MobStats GetMobOnlyStats => _mobStats;
    public MobStats GetBonusOnlyStats => _bonusesStats;
    public MobStats GetDefaultValuesStats => _defaultValues; // для UI, лечения и тп
    public Fraction GetFraction => _mobFraction;
    public Dictionary<BuffStats, float> GetBuffs => BuffStats;

    [SerializeField] private MobStats _mobStats;
    [SerializeField] private MobStats _bonusesStats;
    [SerializeField] private MobStats _defaultValues;
    [SerializeField] private Fraction _mobFraction;

    //тут активированные в данный момент баффы ЭТО НЕ ТЕСТИРОВАЛОСЬ
    [SerializeField] private Dictionary<BuffStats, float> BuffStats = new Dictionary<BuffStats, float>();// <бафф, время действия>

    void Awake()
    {
        _defaultValues = _mobStats ?? ScriptableObject.CreateInstance<MobStats>();
        _mobStats = Instantiate(_defaultValues);
        _bonusesStats = ScriptableObject.CreateInstance<MobStats>();
    }

    void Start()
    {
        MobStatsUpdated?.Invoke(GetStats);
    }

    public float GetAttackTime()
    {
        return Mathf.Max(0.1f, 5f - 0.25f * _mobStats.AttackSpeed);
    }

    /// <summary>
    /// Рассчет из очков силы отталкивания (RepulsivePush) в вектор ускорения. Принимает нормализованный вектор
    /// </summary>
    public Vector2 GetRepulsivePushVector(Vector2 normalizedDirection)
    {
        normalizedDirection.Normalize();
        return normalizedDirection * (10f / 0.5f * _mobStats.RepulsivePush);
    }

    /// <summary>
    /// Рассчет урона
    /// </summary>
    public void AddDamage(int damage)
    {
        _mobStats.Health -= Mathf.Max(0, damage - _mobStats.Defense);
        MobStatsUpdated?.Invoke(GetStats);
    }

    public void Heal(int points)
    {
        _mobStats.Health = Mathf.Min(_mobStats.Health + points, _defaultValues.Health);
        MobStatsUpdated?.Invoke(GetStats);
    }

    public void AddBonuses(MobStats val)
    {
        _bonusesStats = _bonusesStats + val;
        MobStatsUpdated?.Invoke(GetStats);
    }

    public void AddBuff(BuffStats val)
    {
        //todo : оптимизировать
        //Проверка на наличие одинакого по MobStats баффа
        foreach (var i in BuffStats)
        {
            if (i.Key.MobStats == val.MobStats)
            {
                //если возможно, суммирование времени действия
                if (val.ActionTimeCanSum && i.Key.ActionTimeCanSum)
                {
                    var timeLeft = i.Value;
                    BuffStats.Remove(i.Key);
                    BuffStats.Add(val, timeLeft + val.ActionTime);
                }
                else
                {
                    BuffStats.Remove(i.Key);
                    BuffStats.Add(val, Time.time + val.ActionTime);
                }
                BuffsUpdated.Invoke(BuffStats);
                return;
            }
        }
        //если одинакого не нашлось, просто добавляется новый
        BuffStats.Add(val, Time.time + val.ActionTime);
        BuffsUpdated?.Invoke(BuffStats);
    }

    void Update()
    {
        //проверка времени действия
        if (1 > BuffStats.Count) 
            return;

        foreach (var i in BuffStats)
        {
            if (i.Value <= Time.time)
            {
                BuffStats.Remove(i.Key);
                BuffsUpdated?.Invoke(BuffStats);
            }
        }
    }

    public void ResetBonuses()
    {
        _bonusesStats = new MobStats();
        MobStatsUpdated?.Invoke(GetStats);
    }

    public void ResetBuffs()
    {
        BuffStats.Clear();
        BuffsUpdated.Invoke(BuffStats);
    }
}

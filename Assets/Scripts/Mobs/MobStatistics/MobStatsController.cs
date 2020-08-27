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

    public MobStats GetStats => MobStats + BonusesStats;
    public MobStats GetMobOnlyStats => MobStats;
    public MobStats GetBonusOnlyStats => BonusesStats;
    public Dictionary<BuffStats, float> GetBuffs => BuffStats;

    [SerializeField] private MobStats MobStats;
    [SerializeField] private MobStats BonusesStats;

    //тут активированные в данный момент баффы ЭТО НЕ ТЕСТИРОВАЛОСЬ
    [SerializeField] private Dictionary<BuffStats, float> BuffStats = new Dictionary<BuffStats, float>();// <бафф, время действия>

    void Awake()
    {
        MobStats = MobStats ?? new MobStats();
        BonusesStats = new MobStats();
    }

    public void AddBonuses(MobStats val)
    {
        BonusesStats = BonusesStats + val;
        MobStatsUpdated.Invoke(GetStats);
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
        BuffsUpdated.Invoke(BuffStats);
    }

    void Update()
    {
        //проверка времини действия
        if (BuffStats.Count > 0)
        {
            foreach (var i in BuffStats)
            {
                if (i.Value <= Time.time)
                {
                    BuffStats.Remove(i.Key);
                    BuffsUpdated.Invoke(BuffStats);
                }
            }
        }
    }

    public void ResetBonuses()
    {
        BonusesStats = new MobStats();
        MobStatsUpdated.Invoke(GetStats);
    }

    public void ResetBuffs()
    {
        BuffStats.Clear();
        BuffsUpdated.Invoke(BuffStats);
    }
}

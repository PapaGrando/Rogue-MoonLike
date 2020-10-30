using UnityEngine;

[CreateAssetMenu(fileName = "New MobStats", menuName = "Mob Stats", order = 51)]
public class MobStats : ScriptableObject
{
    //todo : обсудить статы, их расчеты, макс - мин значения
    public int Attack;
    public int AttackSpeed; // 1 = 3сек  каждое очко сокращает на 0.25 . Рассчет в MobStatsController    
    public int Defense;
    public int RepulsivePush; //сила отталкивания при ударе

    public int Health;
    public int Strength;

    public int Speed;

    public MobStats()
    {
        Health = 0;
        AttackSpeed = 0;
        Attack = 0;
        Defense = 0;
        RepulsivePush = 0;
        Strength = 0;
        Speed = 0;
    }

    /// <summary>
    /// Возвращает MobStats с параментрами по умолчанию
    /// Health = 100,
    /// Attack = 1,
    /// Defense = 1,
    /// Strength = 1,
    /// AttackSpeed = 1;
    /// Speed = 1,
    /// </summary>
    public static MobStats Defaults()
    {
        return new MobStats()
        {
            Health = 100,
            Attack = 1,
            Defense = 1,

            RepulsivePush = 1,

            Strength = 1,
            AttackSpeed = 1,

            Speed = 1,
        };
    }

    public static MobStats operator +(MobStats a, MobStats b)
    {
        return new MobStats
        {
            Health = a.Health + b.Health,
            Attack = a.Attack + b.Attack,
            Defense = a.Defense + b.Defense,
            RepulsivePush = a.RepulsivePush + b.RepulsivePush,

            Strength = a.Strength + b.Strength,
            AttackSpeed = a.AttackSpeed + a.AttackSpeed,

            Speed = a.Speed + b.Speed,
        };
    }

    public static MobStats operator -(MobStats a, MobStats b)
    {
        return new MobStats
        {
            Health = a.Health - b.Health,
            Attack = a.Attack - b.Attack,
            Defense = a.Defense - b.Defense,
            RepulsivePush = a.RepulsivePush - b.RepulsivePush,

            Strength = a.Strength - b.Strength,
            AttackSpeed = a.AttackSpeed - a.AttackSpeed,

            Speed = a.Speed - b.Speed,
        };
    }
}
using System;
using UnityEngine;

[Serializable]
public class MobStats : ScriptableObject
{
    public int Health;
    public int Attack;
    public int Defense;

    public int Strength;
    public int Agility;
    public int Intelligence;

    public MobStats()
    {
        Health = 1;
        Attack = 1;
        Defense = 1;

        Strength = 1;
        Agility = 1;
        Intelligence = 1;
    }
}
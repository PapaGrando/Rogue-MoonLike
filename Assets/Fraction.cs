using UnityEngine;

[CreateAssetMenu(fileName = "New Fraction", menuName = "Fraction", order = 51)]
public class Fraction : ScriptableObject
{
    [SerializeField] protected Fraction[] Friendly;
    [SerializeField] protected Fraction[] Enemy;

    public Fraction[] GetFriends => Friendly;
    public Fraction[] GetEnemies => Enemy;

    public bool IsEnemy(Fraction fraction)
    {
        for (int i = 0; i < Enemy.Length; i++)
        {
            if (Enemy[i].name == fraction.name)
                return true;
        }

        return false;
    }
}

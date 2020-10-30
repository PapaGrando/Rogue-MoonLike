using UnityEngine;

public delegate void EventHandlerIMob(); 
public interface IMob
{
    event EventHandlerIMob Dead;

    MobStatsController GetMobStatsController(); //чтоб не делать лишний getComponent при рассчете атаки и тд
    void Idle();
    void Damage(int damage, Vector2 pushPower); //pushPower - вектор силы отталкивания 
    void Death();
    void Attack();
    void SetDirection(Direction direction);
}
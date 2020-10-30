public delegate void AnimationActionEventHandler();

public interface IMobAnimatable
{
    event AnimationActionEventHandler AttackAnimationEnded; 
    event AnimationActionEventHandler DeathAnimationEnded;
    event AnimationActionEventHandler DamageAnimationEnded;
    void Idle();
    void Damage();
    void Death();
    void SwitchSide(Direction direction);
    void Attack();
}
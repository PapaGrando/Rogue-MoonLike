public interface IMobAnimatable
{
    void Idle();
    void Damage();
    void Death();
    void SwitchSide(Direction direction);
    void Attack();
}
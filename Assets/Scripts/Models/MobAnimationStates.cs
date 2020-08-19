using System;

[Serializable]
public class MobAnimationStates
{
    public bool IsAttacking;
    public bool IsDamaging;
    public bool IsDead;
    public bool IsFalling;
    public bool IsIdle;
    public bool IsJumping;
    public bool IsRunning;
    public bool IsSpecialAttacking;
    public bool IsSpecialMove;
    public bool OnSpecialState;

    public MobAnimationStates()
    {
        IsIdle = false;
        IsDamaging = false;
        IsDead = false;
        IsRunning = false;
        IsJumping = false;
        IsFalling = false;
        IsAttacking = false;
        OnSpecialState = false;
        IsSpecialMove = false;
        IsSpecialAttacking = false;
    }
}
using System;

[Serializable]
public class MobAnimationStates
{
    public bool IsIdle;
    public bool IsDamaging;
    public bool IsDead;
    public bool IsRunning;
    public bool IsJumping;
    public bool IsFalling;
    public bool IsAttacking;
    public bool OnSpecialState;
    public bool IsSpecialMove;
    public bool IsSpecialAttacking;

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
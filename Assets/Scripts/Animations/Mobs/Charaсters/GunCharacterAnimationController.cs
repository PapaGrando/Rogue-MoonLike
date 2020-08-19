using System.Collections;
using UnityEngine;

public class GunCharacterAnimationController : MobAnimationController, IMobAnimatable, IMobMoveAnimatable, IMobSpecialAnimatable
{
    public void SwitchSide(Direction direction)
    {
        if (MobAnimationStates.IsDead) return;

        Direction = direction;
        SetDirection(Direction);
    }

    public void Idle()
    {
        MobAnimationStates = new MobAnimationStates
        {
            IsIdle = true
        };

        SetState(MobAnimationStates);
    }

    public void Damage()
    {
        MobAnimationStates = new MobAnimationStates
        {
            IsDamaging = true
        };

        SetState(MobAnimationStates);
        StartCoroutine("DamageDelay", 0.5f);
    }

    public void Death()
    {
        MobAnimationStates = new MobAnimationStates
        {
            IsDead = true
        };

        SetState(MobAnimationStates);
    }

    public void Attack()
    {
        MobAnimationStates = new MobAnimationStates
        {
            IsAttacking = true,
            IsSpecialAttacking = true
        };

        SetState(MobAnimationStates);
    }

    public void Run()
    {
        MobAnimationStates = new MobAnimationStates
        {
            IsRunning = true,
            IsSpecialMove = true
        };

        SetState(MobAnimationStates);
    }

    public void Jump()
    {
        MobAnimationStates = new MobAnimationStates
        {
            IsJumping = true
        };

        SetState(MobAnimationStates);
    }

    public void Fall()
    {
        MobAnimationStates = new MobAnimationStates
        {
            IsFalling = true
        };

        SetState(MobAnimationStates);
    }

    public void SpecialAttack()
    {
        MobAnimationStates = new MobAnimationStates
        {
            IsSpecialAttacking = true
        };

        SetState(MobAnimationStates);
    }

    public void SpecialMove()
    {
        MobAnimationStates = new MobAnimationStates
        {
            IsSpecialMove = true
        };

        SetState(MobAnimationStates);
    }

    public void SpecialStateSwitch(bool val)
    {
        MobAnimationStates.OnSpecialState = val;
        SetState(MobAnimationStates);
    }

    protected override void SetState(MobAnimationStates mobAnimationStates)
    {
        Animator.SetBool("Run", mobAnimationStates.IsRunning || mobAnimationStates.IsSpecialMove);
        Animator.SetBool("Damage", mobAnimationStates.IsDamaging);
        Animator.SetBool("Death", mobAnimationStates.IsDead);
        Animator.SetBool("GunUp", mobAnimationStates.OnSpecialState);
        Animator.SetBool("Jump", mobAnimationStates.IsJumping);
        Animator.SetBool("Fall", mobAnimationStates.IsFalling);
        Animator.SetBool("Attack", mobAnimationStates.IsAttacking || mobAnimationStates.IsSpecialAttacking);
    }

    private IEnumerable DamageDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        MobAnimationStates.IsDamaging = false;

        SetState(MobAnimationStates);
    }
}
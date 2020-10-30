using System.Collections;
using UnityEngine;

public class MobAnimationController : MonoBehaviour
{
    public event AnimationActionEventHandler AttackAnimationEnded;
    public event AnimationActionEventHandler DeathAnimationEnded;
    public event AnimationActionEventHandler DamageAnimationEnded;

    public MobAnimationStates MobAnimationStates { get; protected set; } = new MobAnimationStates();
    public Direction Direction { get; protected set; } = Direction.None;
    
    protected Animator Animator;
    protected SpriteRenderer SpriteRenderer;

    [SerializeField] protected bool InvertedSide = false;

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SetState(MobAnimationStates);
    }

    protected virtual void SetState(MobAnimationStates mobAnimationStates) { }

    protected virtual void SetDirection(Direction direction)
    {
        if (Direction == Direction.Right)
            SpriteRenderer.flipX = false;
        else if (Direction == Direction.Left)
            SpriteRenderer.flipX = true;

        if (InvertedSide) SpriteRenderer.flipX = !SpriteRenderer.flipX;
    }

    protected virtual IEnumerable DamageDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        MobAnimationStates.IsDamaging = false;

        SetState(MobAnimationStates);
    }

    //методы вызывается из Аниматора. 
    public void AttackAnimationEndedActionInvoke() => AttackAnimationEnded?.Invoke();

    public void DeathAnimationEventEndedActionInvoke() => DeathAnimationEnded?.Invoke();

    public void DamageAnimationEventEndedActionInvoke() => DamageAnimationEnded?.Invoke();
}
using UnityEngine;
using UnityEngine.Events;

public class MobAnimationController : MonoBehaviour
{
    public UnityAction AttackAnimationEnded;
    public MobAnimationStates MobAnimationStates { get; protected set; } = new MobAnimationStates();
    public Direction Direction { get; protected set; } = Direction.None;

    protected Animator Animator;
    protected SpriteRenderer SpriteRenderer;

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
    }

    //метод вызывается из Аниматора. 
    public virtual void AttackAnimationEndedActionInvoke() => AttackAnimationEnded.Invoke();
}
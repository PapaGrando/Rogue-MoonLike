using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour, IMob
{
    public event EventHandlerIMob Dead;

    protected bool IsAttacking;

    protected IMobAnimatable ImobAnimatable;
    protected Rigidbody2D Rigidbody2D;
    protected float NextAttackTime;
    protected BoxCollider2D BoxCollider;
    protected Direction MobDirection = Direction.Right;
    protected SpriteRenderer SpriteRenderer;
    [Range(0, 100)][SerializeField] protected float DestroyDelayAfterDeath = 2f;
    [SerializeField] protected MobStatsController MobStatsController;
    [SerializeField] protected Vector2 Velosity;

    protected virtual void Awake()
    {
        ImobAnimatable = GetComponent<IMobAnimatable>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        MobStatsController = GetComponent<MobStatsController>();

        SpriteRenderer = GetComponent<SpriteRenderer>();
        if (ImobAnimatable != null)
        {
            ImobAnimatable.DeathAnimationEnded += OnDeathAnimationEnded;
            ImobAnimatable.AttackAnimationEnded += OnAttackAnimationEnded;
        }
    }

    public MobStatsController GetMobStatsController() => MobStatsController;

    public virtual void Idle()
    {
        ImobAnimatable.Idle();
    }

    public virtual void Damage(int damage, Vector2 pushPower)//pushpower - вектор отталкивания отталкивания
    {
        ImobAnimatable?.Damage();
        GetMobStatsController().AddDamage(damage);

        if (GetMobStatsController().GetStats.Health <= 0)
            Death();
    }

    public virtual void Death()
    {
        ImobAnimatable.Death();
        Dead?.Invoke();

        Rigidbody2D.velocity = Vector2.zero;
    }

    public virtual void Attack()
    {
        //Определение стороны атаки от текущего положения моба
        Vector2 attackDirection = Vector2.zero;
        if (MobDirection == Direction.Right)
            attackDirection = Vector2.right;
        else if (MobDirection == Direction.Left)
            attackDirection = Vector2.left;

        IsAttacking = true;
        ImobAnimatable.Attack();

        //raycast'im объекты на layermask "Mobs" в зоне коллайдера
        var targets = Physics2D.RaycastAll(BoxCollider.bounds.center, attackDirection, BoxCollider.bounds.size.x / 2 + 0.1f);

        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {  
                //игнорируем себя
               if(targets[i].collider.gameObject.GetInstanceID() == gameObject.GetInstanceID()) continue;

               var targetIMob = targets[i].collider.gameObject.GetComponent<IMob>();
               if (targetIMob != null)
               {
                   //проверяем фракцию
                   if (GetMobStatsController().GetFraction
                       .IsEnemy(targetIMob.GetMobStatsController().GetFraction))
                   {
                       //атакуем и отталкиванием
                        targetIMob.Damage(MobStatsController.GetStats.Attack, MobStatsController.GetRepulsivePushVector(attackDirection));

                   }
               }
            }
        }
    }

    public virtual void SetDirection(Direction direction)
    {
        ImobAnimatable.SwitchSide(direction);
        MobDirection = direction;
    }

    protected virtual void OnAttackAnimationEnded()
    {
        IsAttacking = false;
    }

    protected virtual void OnDeathAnimationEnded()
    {
        Destroy(gameObject,DestroyDelayAfterDeath);
        //StartCoroutine(SmoothDestroy(DestroyDelayAfterDeath));
    }

    protected virtual IEnumerator SmoothDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);

        if(SpriteRenderer != null)
        {
            while (SpriteRenderer.color.a > 0)
            {
                SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, 
                    SpriteRenderer.color.r - 0.05f);

                yield return new WaitForSeconds(0.05f);
            }
        }
        Destroy(gameObject);
    }

    protected virtual void Start()
    {

    }

    protected virtual void FixedUpdate()
    {

    }
}
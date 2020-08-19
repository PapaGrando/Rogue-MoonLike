using UnityEngine;

public class MobController : MonoBehaviour, IMob
{
    protected IMobAnimatable ImobAnimatable;
    protected Rigidbody2D Rigidbody2D;
    protected float NextAttackTime;
    [SerializeField] protected MobStatsController MobStatsController;
    [SerializeField] protected Vector2 Velosity;

    [SerializeField] private bool AutoAttack = true;

    protected virtual void Awake()
    {
        ImobAnimatable = GetComponent<IMobAnimatable>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        MobStatsController = GetComponent<MobStatsController>();
    }

    public virtual void Idle()
    {
        ImobAnimatable.Idle();
    }

    public virtual void Damage(float damage)
    {
        ImobAnimatable.Damage();
    }

    public virtual void Death()
    {
        ImobAnimatable.Death();
    }

    public virtual void Attack()
    {}

    protected virtual void Start()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
    }

    protected virtual void FixedUpdate()
    {

    }
}
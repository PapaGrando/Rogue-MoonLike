using UnityEngine;

public class MobController : MonoBehaviour, IMob
{
    [SerializeField] protected MobStats MobStats;

    protected IMobAnimatable ImobAnimatable;

    protected virtual void Start()
    {
        ImobAnimatable = GetComponent<IMobAnimatable>();
    }

    public virtual void Idle()
    {
        ImobAnimatable.Idle();
    }

    public virtual void Damage()
    {
        ImobAnimatable.Damage();
    }

    public virtual void Death()
    {
        ImobAnimatable.Death();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {

    }

    protected virtual void Update()
    {

    }
}
using UnityEngine;

public class MovingMobController : MobController, IMobMovable
{
    protected Rigidbody2D _rigidbody2D;
    protected IMobMoveAnimatable IMobMoveAnimatable;
    [SerializeField] private Vector2 _velosity;
    [SerializeField] private float _gravity; //сделать константой

    protected override void Start()
    {
        base.Start();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        IMobMoveAnimatable = GetComponent<IMobMoveAnimatable>();
    }

    public virtual void Run(float speed, Direction direction)
    {
        IMobMoveAnimatable.Run();
        ImobAnimatable.SwitchSide(direction);
        
        if (direction == Direction.Right)
            _velosity = new Vector2(speed, _velosity.y);
        else if (direction == Direction.Left)
            _velosity = new Vector2(-speed, _velosity.y);

        _rigidbody2D.velocity = _velosity;
    }

    public virtual void Jump(float power)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,power) * Time.fixedDeltaTime;
    }

    public virtual void Fall()
    {
        IMobMoveAnimatable.Fall();

        _velosity = new Vector2(_velosity.x, _velosity.y + _gravity) * Time.fixedDeltaTime;
        _rigidbody2D.velocity = _velosity;
    }

    protected virtual void FixedUpdate()
    {
        if (_rigidbody2D.velocity != _velosity)
            _rigidbody2D.velocity = _velosity;
    }
}

using UnityEngine;

public class MovingMobController : MobController, IMobMovable
{
    //Делегат EventHandler берется из интерфейса IMobMovable
    public event EventHandlerIMobMovable IsGroundedEvent;
    public event EventHandlerIMobMovable IsNearWallEvent;

    protected bool IsGrounded;
    protected bool IsNearWall;
    protected bool IsRunning;
    protected bool IsJumping;
    protected IMobMoveAnimatable IMobMoveAnimatable;

    private LayerMask _platformLayerMask;
    private Vector2 _lastPos = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();
        IMobMoveAnimatable = GetComponent<IMobMoveAnimatable>();
        _platformLayerMask = LayerMask.GetMask("Platforms");
    }

    public virtual void Run(Direction direction)
    {
        if (IsGrounded) IMobMoveAnimatable.Run();
        ImobAnimatable.SwitchSide(direction);

        IsRunning = true;
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (direction == Direction.Right)
            Velosity = new Vector2(MobStatsController.GetStats.Speed, Velosity.y);
        else if (direction == Direction.Left)
            Velosity = new Vector2(-MobStatsController.GetStats.Speed, Velosity.y);
    }
    //todo: стейт машина, волны, уровень, фикс анимаций, гейм контроллер, собрать проект, закоммитить
    public virtual void Jump()
    {
        if (!IsGrounded) return;

        IsJumping = true;

        Velosity = Vector2.up * (MobStatsController.GetStats.Speed * 5);
    }

    //force fall, вызывать из вне не нужно, рассчитывается в fixedUpdate
    public virtual void Fall()
    {
        IMobMoveAnimatable.Fall();
        IsJumping = false;
    }

    public override void Idle()
    {
        if(IsGrounded) ImobAnimatable.Idle();

        IsRunning = false;

        Velosity = new Vector2(0, Velosity.y);
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Rigidbody2D.velocity != Velosity)
            Rigidbody2D.velocity = Velosity;

        IsGrounded = CheckGround();
        IsNearWall = CheckWalls();

        if (!IsGrounded)
        {
            if (!IsAttacking)
            {
                if (Rigidbody2D.velocity.y >= 0)
                    IMobMoveAnimatable.Jump();
                else
                    Fall();
            }

            //гравитация при падении
            Velosity = new Vector2(Velosity.x, Velosity.y - 1);
        }
        else 
        {
            if (!IsRunning && !IsAttacking)
                ImobAnimatable.Idle();

            if (!IsJumping)
                Velosity = new Vector2(Velosity.x, 0);
        }
        
        _lastPos = transform.position;
    }

    //todo : оптимизировать CheckGround и CheckWalls (Physics2D.Overlaps)
    private bool CheckGround()
    {
        //false, если коллайдер не обнаружен
        var result = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size, 0, Vector2.down,
            (BoxCollider.bounds.extents.y + 0.1f), _platformLayerMask).collider != null;

        //событие вызывается при изменении параметра, а не при каждом fixedUpdate
        if (result != IsGrounded)
            IsGroundedEvent?.Invoke(result);

        return result;
    }
    
    private bool CheckWalls()
    {
        var checkLeft = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size / 2, 0, Vector2.left,
            (BoxCollider.bounds.extents.x + 0.1f), _platformLayerMask).collider != null;
        var checkRight = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size / 2, 0, Vector2.right, 
            (BoxCollider.bounds.extents.x + 0.1f), _platformLayerMask).collider != null;

        var result = checkRight || checkLeft;

        if (result != IsNearWall)
            IsNearWallEvent?.Invoke(result);

        return result;
    }
}
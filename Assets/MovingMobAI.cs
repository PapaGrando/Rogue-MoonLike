using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMobAI : MobAI
{
    public bool StopMoving = false;

    protected Vector2 MovePos;

    protected IMobMovable IMobMovable;
    protected bool IsRunning;       // для оптимизации;

    private readonly Vector2 _posReachedOffset = new Vector2(0.5f, 0.5f);
    private readonly Vector2 _posIgnorArea = new Vector2(0.01f, 0.01f);

    protected override void Awake()
    {
        base.Awake();

        IMobMovable = GetComponent<IMobMovable>();

        if (IMobMovable != null)
        {
            IMobMovable.IsNearWallEvent += OnMobNearWall;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (StopMoving) return;

        if (Target != null)
        {
            MovePos = Target.transform.position;

            //Прверка достижения позиции мобом
            if (IsRunning && Mathf.Abs(transform.position.x - MovePos.x) <= _posReachedOffset.x)
                StopMovingX();
            //Проверка, не изменила ли цель пизицию
            else if (!IsRunning && Mathf.Abs(transform.position.x - MovePos.x) >= _posReachedOffset.x)
                MoveToPosX(MovePos);
        }
    }

    private void OnMobNearWall(bool value)
    {
        if (value) StopMovingX();
    }

    private void MoveToPosX(Vector2 position)
    {
        MovePos = position;

        IsRunning = true;

        if (transform.position.x + _posIgnorArea.x > position.x)
            IMobMovable.Run(Direction.Left);
        else if (transform.position.x - _posIgnorArea.x < position.x)
            IMobMovable.Run(Direction.Right);
    }

    private void StopMovingX()
    {
        MovePos = gameObject.transform.position;
        IsRunning = false;
        IMob.Idle();
    }
}

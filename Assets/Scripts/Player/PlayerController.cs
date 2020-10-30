using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private TouchPadController _touchPad;

    private IMob _iMob;
    private IMobMovable _iMobMovable;
    private IMobSpecial _iMobSpecial;
    private readonly Vector2 _posIgnorArea = new Vector2(0.01f, 0.01f);
    private readonly Vector2 _posReachedOffset = new Vector2(0.2f, 0.2f);

    private bool _isRunning = false;
    private bool _special = false;
    private Vector2 _targetPos;
    private MobStatsController _mobStatsController;

    private void Awake()
    {
        _iMob = GetComponent<IMob>();
        _iMobMovable = GetComponent<IMobMovable>();
        _iMobSpecial = GetComponent<IMobSpecial>();
        _mobStatsController = GetComponent<MobStatsController>();

        _touchPad = _touchPad ?? FindObjectOfType<TouchPadController>();

        if (_touchPad != null)
        {
            _touchPad.PositionUpdated.AddListener(MoveToPosX);
            _touchPad.StopMoving.AddListener(StopMovingX);
        }

        if (_iMobMovable != null) _iMobMovable.IsNearWallEvent += WallContact;
    }

    private void Start()
    {
    }

    private void StopMovingX()
    {
        _iMob.Idle();
        _isRunning = false;
    }

    private void MoveToPosX(Vector2 position)
    {
        _targetPos = position;
        _isRunning = true;

        if (transform.position.x + _posIgnorArea.x > position.x)
            _iMobMovable.Run(Direction.Left);
        else if (transform.position.x - _posIgnorArea.x < position.x)
            _iMobMovable.Run(Direction.Right);
    }

    private void WallContact(bool val)
    {
        if (val) StopMovingX();
    }

    void Update()
    {
        //check pos reaching
        if (_isRunning)
        {
            //x
            if (Mathf.Abs(transform.position.x - _targetPos.x) <= _posReachedOffset.x)
            {
                StopMovingX();
            }
        }
    }

    private void OnDestroy()
    {
        if (_touchPad != null)
        {
            _touchPad.PositionUpdated.RemoveListener(MoveToPosX);
            _touchPad.StopMoving.AddListener(StopMovingX);

            if (_iMobMovable != null) _iMobMovable.IsNearWallEvent -= WallContact;
        }
    }
}
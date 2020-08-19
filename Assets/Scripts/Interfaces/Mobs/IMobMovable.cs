public delegate void EventHandler(bool result);

public interface IMobMovable
{
    event EventHandler IsGroundedEvent;
    event EventHandler IsNearWallEvent;

    void Run(Direction direction);
    void Jump();
    void Fall();
}
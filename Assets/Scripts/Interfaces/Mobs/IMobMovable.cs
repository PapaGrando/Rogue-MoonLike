public delegate void EventHandlerIMobMovable(bool result);

public interface IMobMovable
{
    event EventHandlerIMobMovable IsGroundedEvent;
    event EventHandlerIMobMovable IsNearWallEvent;

    void Run(Direction direction);
    void Jump();
    void Fall();
}
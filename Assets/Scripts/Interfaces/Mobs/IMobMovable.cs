public interface IMobMovable
{
    void Run(float speed, Direction direction);
    void Jump(float power);
    void Fall();
}
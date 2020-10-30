using UnityEngine;

public class MobDetectorColliderController : MonoBehaviour
{
    public delegate void ColliderEventHandler(Collider2D collider);
    public event ColliderEventHandler OnTriggerEntered;
    public event ColliderEventHandler OnTriggerExited;

    [TextArea] [SerializeField] private string _editor =
        "Этот компонент передает родителю OnTriggerEnter2D и OnTriggerExit2D, т.к. Родитель находится на другом LayerMask";

    [SerializeField] private BoxCollider2D _parentCollider;

    private void Awake()
    {
        _parentCollider = GetComponentInParent<BoxCollider2D>();

        var collider = GetComponent<BoxCollider2D>();

        collider.size = _parentCollider.size;
        collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collider) => OnTriggerEntered?.Invoke(collider);
    void OnTriggerExit2D(Collider2D collider) => OnTriggerExited?.Invoke(collider);
}

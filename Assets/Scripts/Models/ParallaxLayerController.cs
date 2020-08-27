using UnityEngine;

public class ParallaxLayerController : MonoBehaviour
{
    [Range(1, 0)][SerializeField] private float _parallaxSpeed;

    private float _startPos, _spriteLength;
    private Transform _cameraTransform;

    void Awake()
    {
        _startPos = transform.position.x;
        _spriteLength = GetComponentInChildren<SpriteRenderer>().bounds.size.x;

        _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        var tempPosX = (_cameraTransform.position.x * (1 - _parallaxSpeed));
        var distPosX = (_cameraTransform.position.x * _parallaxSpeed);

        transform.position = new Vector2(_startPos + distPosX, transform.position.y);

        if (tempPosX > _startPos + _spriteLength)
            _startPos += _spriteLength;
        else if (tempPosX < _startPos - _spriteLength)
            _startPos -= _spriteLength;
    }
}

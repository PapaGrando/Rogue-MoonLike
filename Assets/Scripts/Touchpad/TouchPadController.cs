using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchPadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public UnityEventVector2 PositionUpdated = new UnityEventVector2();
    [HideInInspector] public UnityEvent StopMoving = new UnityEvent();

    [SerializeField] private TouchPadMode _touchPadMode;
    [SerializeField] private bool _disabled = false;
    [SerializeField] private float _touchDelay = 0.2f;

    private Image _image;
    private bool _touched;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_disabled) return;
        _touched = true;

        StartCoroutine(TouchDelayExecution());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_disabled) return;
        _touched = false;

        StopCoroutine(TouchDelayExecution());

        if (_touchPadMode == TouchPadMode.LeftRightButtons)
            StopMoving.Invoke();
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SelectMode(TouchPadMode mode)
    {
        _touchPadMode = mode;
    }

    private IEnumerator TouchDelayExecution()
    {
        yield return new WaitForSeconds(_touchDelay);

        if (_touched)
        {
            if (_touchPadMode == TouchPadMode.LeftRightButtons)
                LeftRightButtonsControlls();
            else if (_touchPadMode == TouchPadMode.MoveToTouchedPos)
                MoveToTouchedPosControlls();
        }
    }

    private void MoveToTouchedPosControlls()
    {
#if (UNITY_EDITOR || UNITY_STANDALONE)
        var touch = (Vector2) Input.mousePosition;
#else
        var touch = (Vector2)Input.GetTouch(0).position;
#endif

        var targetPos = (Vector2) Camera.main.ScreenToWorldPoint(touch);
        PositionUpdated.Invoke(targetPos);
    }

    private void LeftRightButtonsControlls()
    {
#if (UNITY_EDITOR || UNITY_STANDALONE)
        var touch = (Vector2) Input.mousePosition;
#else
        var touch = (Vector2)Input.GetTouch(0).position;
#endif
        if (touch.x > Screen.width / 2)
            PositionUpdated.Invoke(new Vector2(float.PositiveInfinity, 0));
        else
            PositionUpdated.Invoke(new Vector2(float.NegativeInfinity, 0));
    }
}
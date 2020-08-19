using UnityEngine;

public class FXActivator : MonoBehaviour
{
    protected Animator Animator;
    [SerializeField] protected bool StartActivated = false;

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.enabled = StartActivated;
    }

    public virtual void EnableFx(bool val)
    {
        Animator.enabled = val;
    }
}
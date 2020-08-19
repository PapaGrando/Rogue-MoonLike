using UnityEngine;

public class FXActivator : MonoBehaviour
{
    [SerializeField] protected bool StartActivated = false;
    protected Animator Animator;

    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.enabled = StartActivated;
    }

    public virtual void EnableFx(bool val) => Animator.enabled = val;
}
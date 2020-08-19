using UnityEngine;

public class FXImpactActivator : FXActivator
{
    [SerializeField] private readonly bool _randomize = false;
    [Range(0, 3)] [SerializeField] private int _type;

    protected override void Start()
    {
        base.Start();
        _type = _randomize ? RandomizeType() : _type;
        Animator.SetInteger("Type", _type);
    }

    public void EnableFx(bool val, int type)
    {
        EnableFx(val);

        if (!val) return;

        Animator.SetInteger("Type", _type);
        Animator.Play("Play");
    }

    public static int RandomizeType()
    {
        return Random.Range(0, 4);
    }
}
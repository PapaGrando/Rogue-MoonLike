using UnityEngine;

//todo : рефактор

public class FXSpeedActivator : FXActivator { }

public class FXImpactActivator : FXActivator
{
    [SerializeField] private bool _randomize = false;
    [Range(0, 3)] [SerializeField] private int _type = 0;

    protected override void Start()
    {
        _type = _randomize ? RandomizeType() : _type;
        Animator.SetInteger("Type", _type);
        base.Start();
    }

    public void EnableFx(bool val, int type)
    {
        EnableFx(val);

        if (!val) return;

        Animator.SetInteger("Type", _type);
        Animator.Play("Play");
    }

    public static int RandomizeType() => Random.Range(0, 4);
}
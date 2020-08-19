using UnityEngine;

public class FXSnowBullet : FXActivator
{
    private ParticleSystem _particleSystem;

    protected override void Start()
    {
        base.Start();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public override void EnableFx(bool val)
    {
        base.EnableFx(val);

        if (val)
            _particleSystem.Play();
        else
            _particleSystem.Stop();
    }
}

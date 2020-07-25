using UnityEngine;

public class InteractableMobController : MobController, IIteracactable
{
    public void Interact<T>(T data)
    {

    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMobAnimatable _mobAnimatable;
    private IMobMoveAnimatable _mobMoveAnimatable;
    private IMobAttackAnimatable _mobAttackAnimatable;
    private IMobSpecialAnimatable _mobSpecialAnimatable;

    private bool _special = false;

    void Start()
    {
        _mobAnimatable = GetComponent<IMobAnimatable>();
        _mobAttackAnimatable = GetComponent<IMobAttackAnimatable>();
        _mobMoveAnimatable = GetComponent<IMobMoveAnimatable>();
        _mobSpecialAnimatable = GetComponent<IMobSpecialAnimatable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _mobAnimatable.SwitchSide(Direction.Left);
            _mobMoveAnimatable.Run();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _mobAnimatable.SwitchSide(Direction.Right);
            _mobMoveAnimatable.Run();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _mobMoveAnimatable.Jump();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _special = !_special;
            _mobSpecialAnimatable.SpecialStateSwitch(_special);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            _mobAnimatable.Death();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            _mobAttackAnimatable.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _mobAnimatable.Idle();
        }
    }
}

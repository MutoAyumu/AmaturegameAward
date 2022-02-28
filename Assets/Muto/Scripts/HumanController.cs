using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] Transform _ghostMovePos = default;
    [SerializeField] PlayerAttack _attack = default;
    public Transform GhostMovePos { get => _ghostMovePos; }

    public override void OnUpdate()
    {
        if (_fire1 && _attack)
        {
            _attack.Attack(_h, _v);
            Debug.Log("UŒ‚‚ªŒÄ‚Î‚ê‚½");
        }
    }
}

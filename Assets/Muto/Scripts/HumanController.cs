using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] PlayerAttack _attack = default;
    [SerializeField] testObjectSearcher _searchar = default;
    [SerializeField] TestMoveTheBlocks _push = default;
    [SerializeField, Tooltip("çUåÇÉ{É^ÉìÇÃñºëO")] string _attackButtonName = "Fire1";

    public override void OnUpdate()
    {
        if(_attack && InputButtonDown(_attackButtonName))
        {
            _attack.Attack(_h, _v);
        }
    }
}

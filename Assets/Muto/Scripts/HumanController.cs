using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] PlayerAttack _attack = default;
    [SerializeField] testObjectSearcher _searchar = default;
    [SerializeField] TestMoveTheBlocks _push = default;
    [SerializeField, Tooltip("UŒ‚ƒ{ƒ^ƒ“‚Ì–¼‘O")] string _attackButtonName = "Fire1";
    [SerializeField, Tooltip("—H—ì‚ªˆÚ“®‚·‚é‚Æ‚«‚ÌŽw’èêŠ")] Transform _ghostSetPos = default;
    [SerializeField] SpriteRenderer _togetherImage = default;

    public Transform GhostSetPos { get => _ghostSetPos;}
    public SpriteRenderer TogetherImage { get => _togetherImage;}

    public override void OnUpdate()
    {
        if(_attack && InputButtonDown(_attackButtonName))
        {
            _attack.Attack(_h, _v);
        }
    }
}

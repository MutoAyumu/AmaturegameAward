using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] PlayerAttack _attack = default;
    [SerializeField] testObjectSearcher _searchar = default;
    [SerializeField] TestMoveTheBlocks _push = default;
    [SerializeField, Tooltip("攻撃ボタンの名前")] string _attackButtonName = "Fire1";
    [SerializeField, Tooltip("幽霊が移動するときの指定場所")] Transform _ghostSetPos = default;
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

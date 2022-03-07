using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] PlayerAttack _attack = default;
    [SerializeField] TestMoveTheBlocks _push = default;
    [SerializeField, Tooltip("�U���{�^���̖��O")] string _attackButtonName = "Fire1";
    [SerializeField, Tooltip("�H�삪�ړ�����Ƃ��̎w��ꏊ")] Transform _ghostSetPos = default;
    [SerializeField] SpriteRenderer _togetherImage = default;
    [SerializeField, Tooltip("Ray���������Ăق����I�u�W�F�N�g�̃��C���[")] LayerMask _layer = default;

    public Transform GhostSetPos { get => _ghostSetPos;}
    public SpriteRenderer TogetherImage { get => _togetherImage;}

    public override void OnUpdate()
    {
        if(_attack && InputButtonDown(_attackButtonName))
        {
            _attack.Attack(_lh, _lv);
        }
    }
    void Activate()
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(_lh, _lv).normalized, _rayLenght, _layer);

        if (hit.collider)
        {
            hit.collider.GetComponent<IHumanGimic>()?.Action();
        }
    }
}

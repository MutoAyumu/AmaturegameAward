using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostController : CharacterControllerBase
{
    public override void OnUpdate()
    {
        //�����Ńv���C���[�ɒǏ]
        if(isFollow)
        {
            this.transform.position = CharacterManager._instance.Player.GhostMovePos.position;
        }
    }

    [SerializeField] float _timeToMove = 1.5f;
    [SerializeField] Collider2D _col = default;
    bool isFollow;

    public bool IsFollow { get => isFollow; set => isFollow = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //�����Ńv���C���[�̉E���Ƃ��Ɉړ�������
            CharacterManager._instance.Vcam.Follow = CharacterManager._instance.Player.transform;
            _isControll = false;    //�����𓮂��Ȃ��悤�ɂ��ăv���C���[�𓮂��悤�ɂ���
            CharacterManager._instance.Player.IsControll = false;
            _col.isTrigger = true;
            CharacterManager._instance.Player.Rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.transform.DOMove(CharacterManager._instance.Player.GhostMovePos.position, _timeToMove)
                .OnComplete(() =>
                {
                    _col.isTrigger = false;
                    CharacterManager._instance.Player.IsControll = true;
                    isFollow = true;
                    CharacterManager._instance.Player.Rb.constraints = RigidbodyConstraints2D.None;
                    CharacterManager._instance.Player.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                });
        }
    }
}

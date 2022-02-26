using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class GhostController : CharacterControllerBase
{
    public override void OnUpdate()
    {
        //�����Ńv���C���[�ɒǏ]
        if(isFollow)
        {
            this.transform.position = CharacterManager._instance.Player.GhostMovePos.position;
        }

        if (Input.GetButtonDown(_inputButton))
        {
            Light2D a = _lightObject.transform.GetChild(0).GetComponent<Light2D>();

            if (a && !isHaveLight)   //���C�g�I�u�W�F�N�g������
            {
                _light = a;
                _isControll = false;
                _light.transform.DOMove(this.transform.position, _timeToMove)
                    .OnComplete(() =>
                    {
                        isHaveLight = true;
                        _light.transform.SetParent(this.transform);
                        _isControll = true;
                    });
            }
            else if (!a && isHaveLight)
            {
                _isControll = false;
                _light.transform.DOMove(_lightObject.transform.position, _timeToMove)
                    .OnComplete(() =>
                    {
                        isHaveLight = false;
                        _light.transform.SetParent(_lightObject.transform);
                        _light.transform.SetSiblingIndex(0);
                        _isControll = true;
                    });
            }
            else
            {
                Debug.Log("���C�g�I�u�W�F�N�g������܂���");
            }
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

    [SerializeField] string _inputButton = " ";
    [SerializeField] string _lightTags = " ";
    [SerializeField] Light2D _light = default;
    [SerializeField] GameObject _lightObject = default;
    bool isHaveLight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_lightTags))
        {
            _lightObject = collision.gameObject;
        }
    }
}

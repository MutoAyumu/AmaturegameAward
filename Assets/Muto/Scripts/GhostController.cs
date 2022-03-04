using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class GhostController : CharacterControllerBase
{

    [SerializeField] float _timeToMove = 1.5f;
    [SerializeField] Collider2D _col = default;
    [SerializeField] string _inputButton = " ";
    [SerializeField] string _lightTags = " ";
    [SerializeField] Light2D _light = default;
    [SerializeField, Tooltip("�ێ��ł�������̏��")] int _upperLimit = 5;
    [SerializeField] testObjectSearcher _searchar = default;
    public int _lightNum;
    bool _isFollow;
    float _lastH;
    float _lastV;
    public bool IsFollow { get => _isFollow; set => _isFollow = value; }
    public Light2D Light { get => _light;}
    public int UpperLimit { get => _upperLimit;}

    /*ToDo
        �H��ɂ��������邱�Ƃ��܂Ƃ߂�
        ���f���}�m�F
    */
    public override void OnUpdate()
    {
        //�����Ńv���C���[�ɒǏ]
        if(_isFollow)
        {
            this.transform.position = CharacterManager._instance.Human.GhostMovePos.position;
        }

        if (_h != 0 || _v != 0)
        {
            if (_lastH != _h || _lastV != _v)
            {
                _lastH = _h;
                _lastV = _v;
                //_anim.SetFloat("X", _lastH);
                //_anim.SetFloat("Y", _lastV);
            }
        }
        //���v�ύX
        Vector2 origin = this.transform.position;
        Debug.DrawLine(origin, origin + new Vector2(_lastH, _lastV), Color.red);
        if (Input.GetButtonDown("Fire2") && !_isFollow && _isControll)
        {
            this.gameObject.GetComponent<TakeTheLightSource>().delivery(_lastH, _lastV);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && _searchar)
        {
            _searchar.Search(_lastH, _lastV);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //�����Ńv���C���[�̉E���Ƃ��Ɉړ�������
            CharacterManager._instance.Vcam.Follow = CharacterManager._instance.Human.transform;
            _isControll = false;    //�����𓮂��Ȃ��悤�ɂ��ăv���C���[�𓮂��悤�ɂ���

            CharacterManager._instance.Human.IsControll = false;
            _col.isTrigger = true;
            CharacterManager._instance.Human.Rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.transform.DOMove(CharacterManager._instance.Human.GhostMovePos.position, _timeToMove)
                .OnComplete(() =>
                {
                    _col.isTrigger = false;
                    CharacterManager._instance.Human.IsControll = true;
                    _isFollow = true;
                    CharacterManager._instance.Human.Rb.constraints = RigidbodyConstraints2D.None;
                    CharacterManager._instance.Human.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                });
        }
    }
}

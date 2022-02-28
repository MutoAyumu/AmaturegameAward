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
    [SerializeField] GameObject _lightObject = default;
    bool _isHaveLight;
    bool _isFollow;
    public bool IsFollow { get => _isFollow; set => _isFollow = value; }

    public override void OnUpdate()
    {
        //ここでプレイヤーに追従
        if(_isFollow)
        {
            this.transform.position = CharacterManager._instance.Player.GhostMovePos.position;
        }

        if (Input.GetButtonDown(_inputButton) && IsControll)
        {
            Light2D a = _lightObject.transform.GetChild(0).GetComponent<Light2D>();

            if (a && !_isHaveLight)   //ライトオブジェクトがある
            {
                _light = a;
                _isControll = false;
                _light.transform.DOMove(this.transform.position, _timeToMove)
                    .OnComplete(() =>
                    {
                        _isHaveLight = true;
                        _light.transform.SetParent(this.transform);
                        _isControll = true;
                    });
            }
            else if (!a && _isHaveLight)
            {
                _isControll = false;
                _light.transform.DOMove(_lightObject.transform.position, _timeToMove)
                    .OnComplete(() =>
                    {
                        _isHaveLight = false;
                        _light.transform.SetParent(_lightObject.transform);
                        _light.transform.SetSiblingIndex(0);
                        _isControll = true;
                    });
            }
            else
            {
                Debug.Log("ライトオブジェクトがありません");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //ここでプレイヤーの右後ろとかに移動させる
            CharacterManager._instance.Vcam.Follow = CharacterManager._instance.Player.transform;
            _isControll = false;    //自分を動かないようにしてプレイヤーを動くようにする

            CharacterManager._instance.Player.IsControll = false;
            _col.isTrigger = true;
            CharacterManager._instance.Player.Rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.transform.DOMove(CharacterManager._instance.Player.GhostMovePos.position, _timeToMove)
                .OnComplete(() =>
                {
                    _col.isTrigger = false;
                    CharacterManager._instance.Player.IsControll = true;
                    _isFollow = true;
                    CharacterManager._instance.Player.Rb.constraints = RigidbodyConstraints2D.None;
                    CharacterManager._instance.Player.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                });
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_lightTags))
        {
            _lightObject = collision.gameObject;
        }
    }
}

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
    public int _lightNum;
    bool _isFollow;
    float _lastH;
    float _lastV;
    public bool IsFollow { get => _isFollow; set => _isFollow = value; }

    public override void OnUpdate()
    {
        //ここでプレイヤーに追従
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
        //※要変更
        Vector2 origin = this.transform.position;
        Debug.DrawLine(origin, origin + new Vector2(_lastH, _lastV), Color.red);
        if (Input.GetKeyDown(KeyCode.Q) && !_isFollow)
        {
            this.gameObject.GetComponent<TakeTheLightSource>().delivery(_lastH, _lastV);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //ここでプレイヤーの右後ろとかに移動させる
            CharacterManager._instance.Vcam.Follow = CharacterManager._instance.Human.transform;
            _isControll = false;    //自分を動かないようにしてプレイヤーを動くようにする

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

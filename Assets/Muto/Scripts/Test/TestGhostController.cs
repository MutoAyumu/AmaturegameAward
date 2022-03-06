using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class TestGhostController : TestCharacterControllerBase
{

    [SerializeField] float _timeToMove = 1.5f;
    [SerializeField] Collider2D _col = default;
    [SerializeField] string _inputButton = " ";
    [SerializeField] string _lightTags = " ";
    [SerializeField] Light2D _light = default;
    [SerializeField, Tooltip("保持できる光源の上限")] int _upperLimit = 5;
    [SerializeField] testObjectSearcher _searchar = default;
    public int _lightNum;
    bool _isFollow;
    float _lastH;
    float _lastV;
    public bool IsFollow { get => _isFollow; set => _isFollow = value; }
    public Light2D Light { get => _light;}
    public int UpperLimit { get => _upperLimit;}

    /*ToDo
        幽霊にだけさせることをまとめる
        モデル図確認
    */
    public override void OnUpdate()
    {
        //ここでプレイヤーに追従
        if(_isFollow)
        {
            this.transform.position = TestCharacterManager._instance.Human.GhostMovePos.position;
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
            //ここでプレイヤーの右後ろとかに移動させる
            TestCharacterManager._instance.Vcam.Follow = TestCharacterManager._instance.Human.transform;
            _isControll = false;    //自分を動かないようにしてプレイヤーを動くようにする

            TestCharacterManager._instance.Human.IsControll = false;
            _col.isTrigger = true;
            TestCharacterManager._instance.Human.Rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.transform.DOMove(TestCharacterManager._instance.Human.GhostMovePos.position, _timeToMove)
                .OnComplete(() =>
                {
                    _col.isTrigger = false;
                    TestCharacterManager._instance.Human.IsControll = true;
                    _isFollow = true;
                    TestCharacterManager._instance.Human.Rb.constraints = RigidbodyConstraints2D.None;
                    TestCharacterManager._instance.Human.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                });
        }
    }
}

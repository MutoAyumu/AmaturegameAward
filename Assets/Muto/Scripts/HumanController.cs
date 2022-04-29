using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] HumanAttack _attack = default;
    [SerializeField] ObjectPusher _push = default;
    [SerializeField, Tooltip("幽霊が移動するときの指定場所")] Transform _ghostSetPos = default;
    [SerializeField] SpriteRenderer _togetherImage = default;
    [SerializeField, Tooltip("Rayが当たってほしいオブジェクトのレイヤー")] LayerMask _layer = default;
    [Header("入力の時に使うボタンの名前")]
    [SerializeField, Tooltip("攻撃ボタンの名前")] string _attackButtonName = "Fire1";
    [SerializeField, Tooltip("物を掴むボタンの名前")] string _grabButtonName = "Fire2";
    [SerializeField] float _grabbingSpeed = 1f;
    [SerializeField] int _humanPower = 1;
    [SerializeField] int _togetherPower = 2;
    int _currentPower;

    public Transform GhostSetPos { get => _ghostSetPos;}
    public SpriteRenderer TogetherImage { get => _togetherImage;}

    public override void OnUpdate()
    {
        if(_push && Input.GetButtonDown(_grabButtonName) && _status != CharacterStatus.ATTACK) //物を掴むときの処理
        {
            var j = _push.Catch(_lh, _lv, _rayLength, _grabbingSpeed, _layer, _anim);

            if(j)
            {
                _status = CharacterStatus.ACTION;
            }
        }
        if(_push && Input.GetButton(_grabButtonName) && _status != CharacterStatus.ATTACK) //物を掴んで動かす時の処理
        {
            _push.MoveIt(_h, _v);
        }   
        else if(_attack && Input.GetButtonDown(_attackButtonName) && _status != CharacterStatus.ATTACK) //攻撃をするときの処理
        {
            _status = CharacterStatus.ATTACK;
            _anim.SetTrigger("IsAttack");
            var power = CharacterManager.Instance.IsTogether ? _togetherPower : _humanPower;
            _attack.Attack(_lh, _lv, power);
        }
        if (_push && Input.GetButtonUp(_grabButtonName)) //物を離す時の処理
        {
            var j = _push.Release(_moveSpeed, _anim);

            if(j)
            {
                _status = CharacterStatus.IDLE;
            }
        }
        if(_lh > 0)
        {
            _ghostSetPos.position = this.transform.position + new Vector3(-0.39f, 0f, 0f);
        }
        else if(_lh < 0)
        {
            _ghostSetPos.position = this.transform.position + new Vector3(0.368f, 0f, 0f);
        }
    }
    
    public void StartAttack()
    {

    }

    public void ResetAttackBool()
    {
        _anim.SetBool("IsAttack", false);
        _status = CharacterStatus.IDLE;
    }
    void Activate()
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(_lh, _lv).normalized, _rayLength, _layer);

        if(hit.collider)
        {
            hit.collider.GetComponent<IHumanGimic>()?.Action();
        }
    }
}

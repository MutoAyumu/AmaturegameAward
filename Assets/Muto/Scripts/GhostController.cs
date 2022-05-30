using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

public class GhostController : CharacterControllerBase
{
    [SerializeField, Tooltip("Rayが当たってほしいオブジェクトのレイヤー")] LayerMask _layer = default;
    [SerializeField, Tooltip("光を取る時に使うボタンの名前")] string _inputLight = "Fire2";
    [SerializeField] LightAbsorption _abs = default;
    [SerializeField, Tooltip("攻撃ボタンの名前")] string _attackButtonName = "Fire1";
    [SerializeField] GhostAttack _attack = default;
    [SerializeField, Tooltip("攻撃する間隔")] float _attackSpeed = 2f;
    [SerializeField] int _attackLimit = 5;
    [SerializeField] CriAtomSource _criAtom = default;

    bool _isFixedRange = default;
    bool _isAttack;
    bool _isPlayAudio;
    int _attackCount = 0;
    float _timer;
    CharacterManager _cm;
    float _alpha;

    //ステートをリセットする
    public event Action ResetStatus;

    public bool IsFixedRange { get => _isFixedRange; set => _isFixedRange = value; }
    public float Alpha { get => _alpha; set => _alpha = value; }

    public override void OnStart()
    {
        ResetStatus = Reset;
        _cm = CharacterManager.Instance;
        _isDamage = false;

        _message.SetMessage(_cm.HumanMessage);
        _alpha = _cm.GhostAlpha;
        _mainSprite.color = new Color(1, 1, 1, _alpha);
    }
    public override void OnUpdate()
    {
        if(Input.GetButtonDown(_inputLight) && _isControll)
        {
            //Activate();
            _status = CharacterStatus.ATTACK;
            _abs.Absorption(_lh, _lv, _rayLength, _layer, _anim, ResetStatus);
        }

        if (Input.GetButtonDown(_attackButtonName) && _attack && _abs.LightCount > 0 && _cm.IsGhostAttack && !_isAttack)
        {
            _attack.Attack(_lh, _lv);
            _anim.SetTrigger("IsAttack");
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "GhostShooting");
            _attackCount++;
            _isAttack = true;

            if (_attackCount >= _attackLimit)
            {
                _attackCount = 0;
                _abs.LightCount--;
                _cm.UILightUpdate(_abs.LightCount);
                DOVirtual.Float(_abs.Light.intensity, _abs.Light.intensity - 1.0f / _abs.Limit, _abs.Time, value => _abs.Light.intensity = value);
            }
        }

        if(_isAttack)
        {
            _timer += Time.deltaTime;

            if(_timer >= _attackSpeed)
            {
                _timer = 0;
                _isAttack = false;
            }
        }

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var dir = new Vector2(h, v).normalized;

        if(dir != Vector2.zero && !_isPlayAudio)
        {
            _isPlayAudio = true;
            SoundManager.Instance.CriAtomPlay(_criAtom ,CueSheet.SE, "GhostMove");
        }
        else if(dir == Vector2.zero && _isPlayAudio)
        {
            _isPlayAudio = false;
            _criAtom.Stop();
        }
    }
    public override void Stop()
    {
        base.Stop();
        _isPlayAudio = false;
        _criAtom.Stop();
    }
    public override void DamageAnim()
    {
        if (_isDamage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            _mainSprite.color = new Color(1f, 1f, 1f, level);
        }
    }

    public override void IsDamageAction()
    {
        base.IsDamageAction();
        _anim.Play("DamageTree");
        _coroutine = StartCoroutine(OnDamage(_alpha));

        _isDamage = true;
    }
    /// <summary>
    /// 幽霊にだけさせる処理の関数
    /// </summary>
    void Activate()
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(_lh, _lv).normalized, _rayLength, _layer);

        if(hit.collider)
        {
            hit.collider.GetComponent<IGhostGimic>()?.Action();
        }
    }
    private void Reset()
    {
        _status = CharacterStatus.IDLE;
    }

    protected override void Interact()
    {
        if (!CharacterManager.Instance.IsTogether)
        {
            var hit = Physics2D.Raycast(this.transform.position, new Vector2(_lh, _lv), _rayLength, _layer);

            if (hit && !IsSetText)
            {
                IsSetText = true;
                CharacterManager.Instance.SetIntaractText(hit.collider?.GetComponent<ISetText>().SetText());
            }
            else if (!hit && IsSetText)
            {
                IsSetText = false;
                CharacterManager.Instance.ClearIntaractText();
            }
        }
        else
        {
            _cm.ClearIntaractText();
        }

        base.Interact();
    }
}

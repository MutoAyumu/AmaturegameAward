using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb = default;
    [SerializeField] string _endAreaTag = "Finish";
    [SerializeField] Animator _anim = default;
    [SerializeField] SpriteRenderer _mainSprite = default;
    [SerializeField] ObjectSearcher _searchar = default;
    [SerializeField, Tooltip("Searchar���ĂԂƂ��̃{�^���̖��O")] string _inputSearchar = "Fire1";
    [SerializeField, Tooltip("Ray�̒���")] protected float _rayLength = 1f;
    [Header("����L�����̃p�����[�^�["), Space(10)]
    [SerializeField] protected float _moveSpeed = 3.0f;

    [Tooltip("�Ō�ɓ��͂��ꂽ�������̒l")]protected float _lh = default;
    public float InputH => _lh;
    [Tooltip("�Ō�ɓ��͂��ꂽ�c�����̒l")] protected float _lv = default;
    public float InputV => _lv;

    [Tooltip("�ǂ����̃L���������삳��Ă��邩�̃t���O")] protected bool _isControll = default;

    protected float _h = default;
    protected float _v = default;

    public bool IsControll { get => _isControll; set => _isControll = value; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public SpriteRenderer MainSprite { get => _mainSprite;}

    private void Update()
    {
        if(_isControll)
        {
            InputValue();
            Move(_h, _v);

            if(InputButtonDown(_inputSearchar) && _searchar)
            {
                Vector2 origin = this.transform.position;
                RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(_lh, _lv), _rayLength, _searchar.Layer);
                _searchar.Search(_lh, _lv, hit);
            }
        }

        OnUpdate();
    }
    /// <summary>
    /// �h����Ŏg��Update
    /// </summary>
    public virtual void OnUpdate()
    {
        
    }
    /// <summary>
    /// �ړ����͂��󂯂�֐�
    /// </summary>
    protected void InputValue()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        if (_h != 0 || _v != 0)
        {
            if (_lh != _h || _lv != _v)
            {
                _lh = _h;
                _lv = _v;

                if (_anim)
                {
                    _anim.SetFloat("X", _lh);
                    _anim.SetFloat("Y", _lv);
                }
            }
        }
    }
    /// <summary>
    /// �{�^���������ꂽ����Ԃ��֐�
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    protected bool InputButtonUp(string button)
    {
        if(Input.GetButtonUp(button))
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// �{�^���������ꂽ����Ԃ��֐�
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    protected bool InputButtonDown(string button)
    {
        if(Input.GetButtonDown(button))
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// �{�^����������Ă��邩��Ԃ��֐�
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    protected bool InputButton(string button)
    {
        if (Input.GetButtonDown(button))
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// ����L�����𓮂����֐�
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    protected void Move(float h, float v)
    {
        var dir = new Vector2(h, v).normalized;
        _rb.velocity = dir * _moveSpeed;

        Debug.DrawRay(this.transform.position, new Vector2(_lh, _lv).normalized * _rayLength, Color.red);
    }
    /// <summary>
    /// ����L�������~�߂�֐�
    /// </summary>
    public void Stop()
    {
        if(_rb)
        {
            _rb.velocity = Vector2.zero;
        }
        else
        {
            Debug.LogError("Rigidbody2D������܂���");
        }
    }
    /// <summary>
    /// �Ō�Ɍ����Ă��������Ԃ��֐�
    /// </summary>
    /// <returns></returns>
    public Vector2 ReturnTheDirection()
    {
        return new Vector2(_lh, _lv);
    }
}

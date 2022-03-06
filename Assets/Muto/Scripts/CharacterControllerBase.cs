using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb = default;
    [SerializeField] string _endAreaTag = "Finish";
    [SerializeField] Animator _anim = default;
    [SerializeField] SpriteRenderer _mainSprite = default;
    [Header("����L�����̃p�����[�^�["), Space(10)]
    [SerializeField] protected float _moveSpeed = 3.0f;

    [Tooltip("�Ō�ɓ��͂��ꂽ�������̒l")]protected float _h = default;
    [Tooltip("�Ō�ɓ��͂��ꂽ�c�����̒l")] protected float _v = default;
    [Tooltip("�ǂ����̃L���������삳��Ă��邩�̃t���O")] protected bool _isControll = default;

    public bool IsControll { get => _isControll; set => _isControll = value; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public SpriteRenderer MainSprite { get => _mainSprite;}

    private void Update()
    {
        if(_isControll)
        {
            Move(InputValueH(), InputValueV());
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
    /// ���ړ����͂��󂯂�֐�
    /// </summary>
    protected float InputValueH()
    {
        var h = Input.GetAxisRaw("Horizontal");

        if(h != 0)//���͂��ꂽ�l��ۑ����Ă���
        {
            _h = h;
        }

        return h;
    }
    /// <summary>
    /// �c�ړ����͂��󂯂�֐�
    /// </summary>
    /// <returns></returns>
    protected float InputValueV()
    {
        var v = Input.GetAxisRaw("Vertical");

        if (v != 0)//���͂��ꂽ�l��ۑ����Ă���
        {
            _v = v;
        }

        return v;
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
    }
    /// <summary>
    /// ����L�������~�߂�֐�
    /// </summary>
    public void Stop()
    {
        if(_rb)
        {
            _rb.Sleep();
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
        return new Vector2(_h, _v);
    }
}

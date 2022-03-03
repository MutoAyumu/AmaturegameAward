using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected float _speed = 3.0f;
    [SerializeField]protected bool _isControll = false;
    [SerializeField] string _endTag = "Finish";
    
    protected float _h = default;
    protected float _v = default;
    protected bool _fire1 = default;
    bool _isStop;
    public bool IsControll { get => _isControll; set => _isControll = value; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }

    /*ToDo
        �ϐ�����������
        ���͂�Base�ɂ܂Ƃ߂�
    */
    void Update()
    {
        if (_isControll)
        {
            InputValue();
            InputFire();
            Move(_h, _v);
        }

        OnUpdate();
    }
    public virtual void OnUpdate()
    {

    }
    /// <summary>
    /// �ړ��̓��͂��󂯎��ׂ̊֐�
    /// </summary>
    protected void InputValue()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }
    /// <summary>
    /// �N���b�N���͂��󂯎��ׂ̊֐�
    /// </summary>
    protected void InputFire()
    {
        _fire1 = Input.GetButtonDown("Fire1");
    }
    /// <summary>
    /// �ړ��p�̊֐�
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    protected void Move(float h, float v)
    {
        Vector2 dir = new Vector2(h, v).normalized;
        _rb.velocity = _speed * dir;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_endTag))
        {
            FieldManager.Instance.Clear();
        }
    }
    public void Stop()
    {
        if (!_isStop)
        {
            _isStop = true;
            _isControll = false;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll; //Sleep�EAwake�ł�邩��
        }
        else
        {
            _isStop = false;
            _isControll = true;
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected float _speed = 3.0f;
    [SerializeField]protected bool _isControll = false;
    
    protected float _h = default;
    protected float _v = default;
    public bool IsControll { get => _isControll; set => _isControll = value; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }

    void Update()
    {
        if (_isControll)
        {
            InputValue();
            Move(_h, _v);
        }
        OnUpdate();
    }
    public virtual void OnUpdate()
    {

    }
    protected void InputValue()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }
    protected void Move(float h, float v)
    {
        Vector2 dir = new Vector2(h, v).normalized;
        _rb.velocity = _speed * dir;
    }
}
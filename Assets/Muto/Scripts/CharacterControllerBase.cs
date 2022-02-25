using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected float _speed = 3.0f;
    [SerializeField] Transform _ghostMovePos = default;

    [SerializeField]protected bool _isControll = false;
    public bool IsControll { get => _isControll; set => _isControll = value; }
    public Transform GhostMovePos { get => _ghostMovePos;}
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }

    void Update()
    {
        if (_isControll)
        {
            Move();
        }
        OnUpdate();
    }
    public virtual void OnUpdate()
    {

    }

    protected void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;
        _rb.velocity = _speed * dir;
    }
}
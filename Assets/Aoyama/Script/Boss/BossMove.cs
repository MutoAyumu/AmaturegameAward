using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [Header("移動時のステータス")]
    [SerializeField] float _chaseDistance = 0.5f;
    [SerializeField] float _minMoveSpeed = 1;
    [SerializeField] float _maxMoveSpeed = 2;
    [SerializeField] bool _isMove = true;
    [Header("移動するTransform")]
    [SerializeField] Transform[] _movePoint;
    [Header("とりあえず参照したいやつ")]
    [SerializeField] Rigidbody2D _rb;

    float _moveSpeedDistance = 0;
    int _currentIndex = 0;
    Vector3 _target;

    void Start()
    {
        ChangeTarget();
        _moveSpeedDistance = _maxMoveSpeed - _minMoveSpeed;
    }

    void Update()
    {
        if(_isMove)
        {
            _target = (_movePoint[_currentIndex].position - transform.position).normalized;
            Move();
        }

        float distance = Vector3.Distance(transform.position, _movePoint[_currentIndex].position);
        if(distance <= _chaseDistance)
        {
            ChangeTarget();
        }
    }

    void Move()
    {
        float moveSpeed = Mathf.PerlinNoise(transform.position.x, 0) * _moveSpeedDistance + _minMoveSpeed;
        _rb.velocity = _target * moveSpeed;
    }

    public void StopMove()
    {
        _isMove = false;
        Debug.Log(_isMove);
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0;
    }

    public void OnMove()
    {
        _isMove = true;
        Debug.Log(_isMove);
    }

    void ChangeTarget()
    {
        _currentIndex = Random.Range(0, _movePoint.Length);
    }
}

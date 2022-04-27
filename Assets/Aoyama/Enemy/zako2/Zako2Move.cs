using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zako2Move : EnemyMove
{
    /// <summary>起きているかのフラグ</summary>
    bool _isWakeUp = false;

    [SerializeField, Tooltip("近づいたら起きる距離")]
    float _noticeDistance = 5f;
    public override void OnFixedUpdate()
    {
        if (_isPause == true)
        {
            return;
        }
        _target = PlayerPosition();

        WakeUp();
        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;

        if (_isWakeUp)
        {
            Move();
            Flip();
        }
    }

    void WakeUp()
    {
        if(_isWakeUp)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, _target);
        Debug.Log(distance);
        if (distance < _noticeDistance)
        {           
            _anim.SetBool("WakeUp",true);
        }
    }

    public void WakeUpFlag()
    {
        _isWakeUp = true;
    }
}

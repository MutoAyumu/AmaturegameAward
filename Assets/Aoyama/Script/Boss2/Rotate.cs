using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] bool _test = false;
    [SerializeField] Transform _testTransform;
    [SerializeField] SpriteRenderer _sprite;

    Vector3 _target;
    CharacterControllerBase _player;
    CharacterControllerBase _ghost;

    void Start()
    {
        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;
        _target = PlayerPosition();

        if (_test)
        {
            _target = _testTransform.position;
        }
    }

    void Update()
    {
        if(transform.position.x < _target.x)
        {
            _sprite.flipX = true;
        }
        else if(transform.position.x > _target.x)
        {
            _sprite.flipX = false;
        }
    }

    Vector3 player1;
    Vector3 player2;
    /// <summary>
    /// 2�l��Player�̂����߂��ق��̍��W��Ԃ�
    /// </summary>
    /// <returns></returns>
    Vector3 PlayerPosition()
    {
        player1 = _player.ColliderCenter();
        Debug.DrawLine(transform.position, player1);
        float isHit1 = Vector3.Distance(transform.position, player1);

        player2 = _ghost.ColliderCenter();
        Debug.DrawLine(transform.position, player2);
        float isHit2 = Vector3.Distance(transform.position, player2);

        if (isHit1 < isHit2)
        {
            Debug.DrawLine(transform.position, player1, Color.red);
            return player1;
        }
        else
        {
            Debug.DrawLine(transform.position, player2, Color.red);
            return player2;
        }
    }
}

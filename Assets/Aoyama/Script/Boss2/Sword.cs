using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] float _power = 0.5f;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] bool _test = false;
    [SerializeField] Transform _testTransform;

    Vector3 _target;
    CharacterControllerBase _player;
    CharacterControllerBase _ghost;
    bool _isShot = false;

    private void Start()
    {
        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;
        _target = PlayerPosition();

        if(_test)
        {
            _target = _testTransform.position;
        }
        _dir = (_target - transform.position).normalized * _power;
        transform.up = _dir;
    }

    void Update()
    {
        Move();
    }

    Vector3 _dir;
    void Move()
    {
        _rb.velocity = _dir;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>() != null)
        {
            collision.gameObject.GetComponent<PlayerHP>().Damage();
        }
    }

    Vector3 player1;
    Vector3 player2;
    /// <summary>
    /// 2êlÇÃPlayerÇÃÇ§ÇøãﬂÇ¢ÇŸÇ§ÇÃç¿ïWÇï‘Ç∑
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [Header("Ç∆ÇËÇ†Ç¶Ç∏éQè∆ÇµÇΩÇ¢Ç‚Ç¬")]
    [SerializeField] Rigidbody2D _rb;

    Vector3 _target = Vector3.zero;
    CharacterControllerBase _player;
    CharacterControllerBase _ghost;

    void Start()
    {
        Destroy(gameObject, 10f);
        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;

        _target = PlayerPosition();
        _target = (_target - transform.position).normalized;
    }

    void Update()
    {
        HomingAttack();
    }

    public void HomingAttack()
    {
        _rb.velocity = _target * _speed;
    }

    Vector3 player1;
    Vector3 player2;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>() != null)
        {
            collision.gameObject.GetComponent<PlayerHP>().Damage();
        }
    }
}

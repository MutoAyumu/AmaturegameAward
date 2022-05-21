using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tama : MonoBehaviour
{
    [SerializeField] float _speed = 1;
    [SerializeField] float _distance = 0.3f;
    [SerializeField] GameObject _bomb;
    [SerializeField] Rigidbody2D _rb;
    [Header("âπ")]
    [SerializeField] string _cueName = "KuroBossBomb2";

    Vector3 _target;
    CharacterControllerBase _player;
    CharacterControllerBase _ghost;

    void Start()
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cueName);
        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;
        _target = PlayerPosition();
    }

    
    void Update()
    {
        Move();
    }

    Vector3 _dir;
    void Move()
    {
        _dir = (_target - transform.position).normalized * _speed;
        _rb.velocity = _dir;

        if(Vector3.Distance(transform.position, _target) < _distance)
        {
            Bomb();
        }
    }

    void Bomb()
    {
        Destroy(gameObject);
        GameObject.Instantiate(_bomb, transform.position, Quaternion.identity);
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

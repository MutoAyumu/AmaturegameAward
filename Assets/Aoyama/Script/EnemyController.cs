using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("�ړ����̃X�e�[�^�X")]
    [SerializeField, Tooltip("�ǂ������鑬�x")]
    float _chaseSpeed = 5f;
    [SerializeField, Tooltip("�ǂ������鋗��")]
    float _chaseDistance = 1;
    [SerializeField, Tooltip("Player��Tag")] 
    string _playerTag = "Player";

    [Header("���C�L���X�g")]
    [SerializeField, Tooltip("Ray���΂�����")]
    int _isHitLength = 50;
    [SerializeField, Tooltip("Ray��������Layer")]
    LayerMask _playerLayer;

    [Header("�Ƃ肠�����Q�Ƃ��������")]
    [SerializeField] Rigidbody2D _rb;

    GameObject[] _players;
    Vector3 _target;

    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag(_playerTag);
        if(!_players[0] || !_players[1])
        {
            Debug.Log("Player��null�ł�");
        }
    }

    void Update()
    {
        EnemyMove();
    }

    /// <summary>
    /// Enemy�̊�{�ړ�
    /// </summary>
    void EnemyMove()
    {
        _target = PlayerPosition();

        //target�Ƃ̋�����chaseDistanse���߂��Ȃ�Ɠ������~�߂�
        float distance = Vector3.Distance(transform.position, _target);
        if (distance < _chaseDistance)
        {
            _target = Vector3.zero;
        }

        _rb.velocity = _target.normalized * _chaseSpeed;
    }

    /// <summary>
    /// 2�l��Player�̂����߂��ق��̍��W��Ԃ�
    /// </summary>
    /// <returns></returns>
    Vector3 PlayerPosition()
    {
        //Player1��Ray���Ƃ΂�
        Vector3 player1 = _players[0].transform.position;
        RaycastHit2D isHit1 = Physics2D.Linecast(transform.position, player1, _playerLayer);

        //Player2��Ray���Ƃ΂�
        Vector3 player2 = _players[1].transform.position;
        RaycastHit2D isHit2 = Physics2D.Linecast(transform.position, player2, _playerLayer);

        if (isHit1.distance < isHit2.distance)
        {
            return isHit1.point;
        }
        else
        {
            return isHit2.point;
        }
    }
}

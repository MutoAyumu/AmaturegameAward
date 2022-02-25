using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("�ړ����̃X�e�[�^�X")]
    [SerializeField, Tooltip("�ǂ������鑬�x")]
    float _chaseSpeed = 5f;
    [SerializeField, Tooltip("�ǂ������鋗��")]
    float _chaseDistance = 1f;
    [SerializeField, Tooltip("�_���[�W���󂯂����̐�����щ���")]
    float _knockBackPower = 5f;
    [SerializeField, Tooltip("Player��Tag")] 
    string _playerTag = "Player";

    [Header("���C�L���X�g")]
    [SerializeField, Tooltip("Ray���΂�����")]
    int _isHitLength = 50;
    [SerializeField, Tooltip("Ray��������Layer")]
    LayerMask _playerLayer;

    [Header("�Ƃ肠�����Q�Ƃ��������")]
    [SerializeField] Rigidbody2D _rb;

    [SerializeField]Transform[] _players;
    Vector3 _target;

    bool first = false;
    void Start()
    {
        first = true;
        //_players = GameObject.FindGameObjectsWithTag(_playerTag);
        if (!_players[0] || !_players[1])
        {
            Debug.Log("Player��null�ł�");
        }
    }

    void FixedUpdate()
    {
        _target = PlayerPosition();

        Move();
    }

    /// <summary>
    /// Enemy�̊�{�ړ�
    /// </summary>
    void Move()
    {
        //target�Ƃ̋�����chaseDistanse���߂��Ȃ�Ɠ������~�߂�
        float distance = Vector3.Distance(transform.position, _target);
        if (distance < _chaseDistance)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        first = true;
        _rb.velocity = (_target - transform.position).normalized * _chaseSpeed;
        //_rb.AddForce((_target - transform.position - (Vector3)_rb.velocity).normalized * _chaseSpeed, ForceMode2D.Force);
    }

    /// <summary>
    /// 2�l��Player�̂����߂��ق��̍��W��Ԃ�
    /// </summary>
    /// <returns></returns>
    Vector3 PlayerPosition()
    {
        ////Player1��Ray���Ƃ΂�
        Vector3 player1 = _players[0].transform.position;
        //RaycastHit2D isHit1 = Physics2D.Linecast(transform.position, player1, _playerLayer);
        //Debug.Log(isHit1.distance);
        //Debug.DrawLine(transform.position, player1);
        float isHit1 = Vector3.Distance(transform.position, player1);

        ////Player2��Ray���Ƃ΂�
        Vector3 player2 = _players[1].transform.position;
        //RaycastHit2D isHit2 = Physics2D.Linecast(transform.position, player2, _playerLayer);
        //Debug.Log(isHit2.distance);
        //Debug.DrawLine(transform.position, player2);
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

    /// <summary>
    /// �_���[�W���󂯂����Ƀm�b�N�o�b�N������
    /// </summary>
    public void KnockBack()
    {
        Debug.Log("�m�b�N�o�b�N");
        Vector3 dir = (transform.position - _target).normalized * _knockBackPower;
        _rb.AddForce(dir, ForceMode2D.Impulse);
    }
}

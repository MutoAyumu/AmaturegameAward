using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy�̈ړ����Ǘ�����N���X
/// </summary>
public class EnemyMove : MonoBehaviour
{
    [Header("�ړ����̃X�e�[�^�X")]
    [SerializeField, Tooltip("�ǂ������鑬�x")]
    float _chaseSpeed = 5f;
    [SerializeField, Tooltip("�ǂ������鋗��")]
    float _chaseDistance = 1f;
    [SerializeField, Tooltip("�_���[�W���󂯂����̐�����щ���")]
    float _knockBackPower = 5f;

    [Header("�Ƃ肠�����Q�Ƃ��������")]
    [SerializeField] Rigidbody2D _rb;

    Vector3 _target;
    CharacterControllerBase _player;
    CharacterControllerBase _ghost;
    bool _isPause = false;

    void Start()
    {

    }
    void FixedUpdate()
    {
        if (_isPause == true)
        {
            return;
        }

        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;

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
            _rb.angularVelocity = 0;
            return;
        }

        _rb.velocity = (_target - transform.position).normalized * _chaseSpeed;
    }

    Vector3 player1;
    Vector3 player2;
    /// <summary>
    /// 2�l��Player�̂����߂��ق��̍��W��Ԃ�
    /// </summary>
    /// <returns></returns>
    Vector3 PlayerPosition()
    {
        player1 = _player.transform.position;
        Debug.DrawLine(transform.position, player1);
        float isHit1 = Vector3.Distance(transform.position, player1);

        player2 = _ghost.transform.position;
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

    Vector3 dir;
    /// <summary>
    /// �_���[�W���󂯂����Ƀm�b�N�o�b�N������
    /// </summary>
    public void KnockBack()
    {
        dir = (transform.position - _target).normalized * _knockBackPower;
        _rb.AddForce(dir, ForceMode2D.Impulse);
    }

    public void Pause()
    {
        _rb.velocity = Vector3.zero;
        _rb.Sleep();
        _isPause = true;
    }

    public void Resume()
    {
        _rb.WakeUp();
        _isPause = false;
    }
}

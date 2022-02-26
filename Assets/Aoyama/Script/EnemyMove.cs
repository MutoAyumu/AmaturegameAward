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

        _rb.velocity = (_target - transform.position).normalized * _chaseSpeed;
    }

    /// <summary>
    /// 2�l��Player�̂����߂��ق��̍��W��Ԃ�
    /// </summary>
    /// <returns></returns>
    Vector3 PlayerPosition()
    {
        Vector3 player1 = CharacterManager._instance.Player.transform.position;
        Debug.DrawLine(transform.position, player1);
        float isHit1 = Vector3.Distance(transform.position, player1);

        Vector3 player2 = CharacterManager._instance.Ghost.transform.position;
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

    /// <summary>
    /// �_���[�W���󂯂����Ƀm�b�N�o�b�N������
    /// </summary>
    public void KnockBack()
    {
        Vector3 dir = (transform.position - _target).normalized * _knockBackPower;
        _rb.AddForce(dir, ForceMode2D.Impulse);
    }
}

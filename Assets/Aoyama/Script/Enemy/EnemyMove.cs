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
    protected float _chaseSpeed = 5f;
    [SerializeField, Tooltip("�߂Â�����")]
    protected float _nearDistance = 1f;
    [SerializeField, Tooltip("�ǂ������n�߂鋗��")]
    protected float _chaseDistance = 8f;
    [SerializeField, Tooltip("�_���[�W���󂯂����̐�����щ���")]
    protected float _knockBackPower = 5f;

    [Header("�Ƃ肠�����Q�Ƃ��������")]
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected SpriteRenderer _sprite;

    [System.NonSerialized]
    public Transform Decoy = null;

    protected Vector3 _target;
    protected CharacterControllerBase _player;
    protected CharacterControllerBase _ghost;
    protected bool _isPause = false;

    /* ToDo
        �_���[�W���󂯂����Ɉ�莞��Move���Ă΂Ȃ��悤�ɂ���
     */

    void Start()
    {
        EnemyManager.Instance.Enemys.Add(gameObject);
        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    public virtual void OnFixedUpdate()
    {
        if (_isPause == true)
        {
            return;
        }

        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;

        _target = PlayerPosition();

        Move();
        Flip();
    }

    /// <summary>
    /// Enemy�̊�{�ړ�
    /// </summary>
    protected void Move()
    {
        float distance = Vector3.Distance(transform.position, _target);
        //target�Ƃ̋�����nearDistanse��艓���Ȃ�Ɠ������~�߂�
        if (distance > _chaseDistance)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0;
            return;
        }
        //target�Ƃ̋�����chaseDistanse���߂��Ȃ�Ɠ������~�߂�
        if (distance < _nearDistance)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0;
            return;
        }

        _rb.velocity = (_target - transform.position).normalized * _chaseSpeed;
    }

    protected void Flip()
    {
        if(_sprite)
        {
            if (_target.x - transform.position.x > 0)
            {
                _sprite.flipX = true;
            }
            else
            {
                _sprite.flipX = false;
            }
        }
    }

    Vector3 player1;
    Vector3 player2;
    /// <summary>
    /// 2�l��Player�̂����߂��ق��̍��W��Ԃ�
    /// </summary>
    /// <returns></returns>
    protected Vector3 PlayerPosition()
    {
        if (Decoy != null)
        {
            Debug.DrawLine(transform.position, Decoy.position);
            return Decoy.position;
        }

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

    Vector3 dir;
    /// <summary>
    /// �_���[�W���󂯂����Ƀm�b�N�o�b�N������
    /// </summary>
    public void KnockBack()
    {
        dir = (transform.position - _target).normalized * _knockBackPower;
        _rb.AddForce(dir, ForceMode2D.Impulse);
    }

    public void SetDecoy(Transform vec)
    {
        Decoy = vec;
    }

    public void ResetDecoy()
    {
        Decoy = null;
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

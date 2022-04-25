using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの移動を管理するクラス
/// </summary>
public class EnemyMove : MonoBehaviour
{
    [Header("移動時のステータス")]
    [SerializeField, Tooltip("追いかける速度")]
    protected float _chaseSpeed = 5f;
    [SerializeField, Tooltip("近づく距離")]
    protected float _nearDistance = 1f;
    [SerializeField, Tooltip("追いかけ始める距離")]
    protected float _chaseDistance = 8f;
    [SerializeField, Tooltip("ダメージを受けた時の吹っ飛び加減")]
    protected float _knockBackPower = 5f;

    [Header("とりあえず参照したいやつ")]
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected SpriteRenderer _sprite;

    [System.NonSerialized]
    public Transform Decoy = null;

    protected Vector3 _target;
    protected CharacterControllerBase _player;
    protected CharacterControllerBase _ghost;
    protected bool _isPause = false;

    /* ToDo
        ダメージを受けた時に一定時間Moveを呼ばないようにする
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
    /// Enemyの基本移動
    /// </summary>
    protected void Move()
    {
        float distance = Vector3.Distance(transform.position, _target);
        //targetとの距離がnearDistanseより遠くなると動きを止める
        if (distance > _chaseDistance)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0;
            return;
        }
        //targetとの距離がchaseDistanseより近くなると動きを止める
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
    /// 2人のPlayerのうち近いほうの座標を返す
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
    /// ダメージを受けた時にノックバックさせる
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

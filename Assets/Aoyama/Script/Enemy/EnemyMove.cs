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
    float _chaseSpeed = 5f;
    [SerializeField, Tooltip("追いかける距離")]
    float _chaseDistance = 1f;
    [SerializeField, Tooltip("ダメージを受けた時の吹っ飛び加減")]
    float _knockBackPower = 5f;

    [Header("とりあえず参照したいやつ")]
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
    /// Enemyの基本移動
    /// </summary>
    void Move()
    {
        //targetとの距離がchaseDistanseより近くなると動きを止める
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
    /// 2人のPlayerのうち近いほうの座標を返す
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
    /// ダメージを受けた時にノックバックさせる
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

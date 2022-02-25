using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("移動時のステータス")]
    [SerializeField, Tooltip("追いかける速度")]
    float _chaseSpeed = 5f;
    [SerializeField, Tooltip("追いかける距離")]
    float _chaseDistance = 1f;
    [SerializeField, Tooltip("ダメージを受けた時の吹っ飛び加減")]
    float _knockBackPower = 5f;
    [SerializeField, Tooltip("PlayerのTag")] 
    string _playerTag = "Player";

    [Header("レイキャスト")]
    [SerializeField, Tooltip("Rayを飛ばす距離")]
    int _isHitLength = 50;
    [SerializeField, Tooltip("Rayが当たるLayer")]
    LayerMask _playerLayer;

    [Header("とりあえず参照したいやつ")]
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
            Debug.Log("Playerがnullです");
        }
    }

    void FixedUpdate()
    {
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
            return;
        }

        first = true;
        _rb.velocity = (_target - transform.position).normalized * _chaseSpeed;
        //_rb.AddForce((_target - transform.position - (Vector3)_rb.velocity).normalized * _chaseSpeed, ForceMode2D.Force);
    }

    /// <summary>
    /// 2人のPlayerのうち近いほうの座標を返す
    /// </summary>
    /// <returns></returns>
    Vector3 PlayerPosition()
    {
        ////Player1にRayをとばす
        Vector3 player1 = _players[0].transform.position;
        //RaycastHit2D isHit1 = Physics2D.Linecast(transform.position, player1, _playerLayer);
        //Debug.Log(isHit1.distance);
        //Debug.DrawLine(transform.position, player1);
        float isHit1 = Vector3.Distance(transform.position, player1);

        ////Player2にRayをとばす
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
    /// ダメージを受けた時にノックバックさせる
    /// </summary>
    public void KnockBack()
    {
        Debug.Log("ノックバック");
        Vector3 dir = (transform.position - _target).normalized * _knockBackPower;
        _rb.AddForce(dir, ForceMode2D.Impulse);
    }
}

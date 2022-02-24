using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("移動時のステータス")]
    [SerializeField, Tooltip("追いかける速度")]
    float _chaseSpeed = 5f;
    [SerializeField, Tooltip("追いかける距離")]
    float _chaseDistance = 1;
    [SerializeField, Tooltip("PlayerのTag")] 
    string _playerTag = "Player";

    [Header("レイキャスト")]
    [SerializeField, Tooltip("Rayを飛ばす距離")]
    int _isHitLength = 50;
    [SerializeField, Tooltip("Rayが当たるLayer")]
    LayerMask _playerLayer;

    [Header("とりあえず参照したいやつ")]
    [SerializeField] Rigidbody2D _rb;

    GameObject[] _players;
    Vector3 _target;

    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag(_playerTag);
        if(!_players[0] || !_players[1])
        {
            Debug.Log("Playerがnullです");
        }
    }

    void Update()
    {
        EnemyMove();
    }

    /// <summary>
    /// Enemyの基本移動
    /// </summary>
    void EnemyMove()
    {
        _target = PlayerPosition();

        //targetとの距離がchaseDistanseより近くなると動きを止める
        float distance = Vector3.Distance(transform.position, _target);
        if (distance < _chaseDistance)
        {
            _target = Vector3.zero;
        }

        _rb.velocity = _target.normalized * _chaseSpeed;
    }

    /// <summary>
    /// 2人のPlayerのうち近いほうの座標を返す
    /// </summary>
    /// <returns></returns>
    Vector3 PlayerPosition()
    {
        //Player1にRayをとばす
        Vector3 player1 = _players[0].transform.position;
        RaycastHit2D isHit1 = Physics2D.Linecast(transform.position, player1, _playerLayer);

        //Player2にRayをとばす
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

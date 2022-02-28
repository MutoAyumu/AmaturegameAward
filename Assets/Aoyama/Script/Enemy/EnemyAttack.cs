using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃判定を行うコライダー")]
    Collider2D _attackCol;

    ContactFilter2D _filter;
    List<Collider2D> _result = new List<Collider2D>(5);

    int count = 0;
    /// <summary>
    /// Enemyの攻撃用の関数
    /// </summary>
    public void Attack()
    {
        //[ToDo] ContactFilter2DをSerializeすることでLayerMaskを指定できるので、余裕があればする
        count = _attackCol.OverlapCollider(_filter, _result);
        _result.ForEach(go => go.GetComponent<PlayerHp>()?.Damage());
    }
}

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃の当たり判定を行うコライダー")]
    Collider2D _attackCol;

    List<Collider2D> _result = new List<Collider2D>(10);

    /// <summary>
    /// 引数のコライダーの範囲内にいる、IDamageのDamage()を呼ぶ関数
    /// </summary>
    public void Attack()
    {
        int count = _attackCol.OverlapCollider(new ContactFilter2D(), _result);
        _result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
    }
}

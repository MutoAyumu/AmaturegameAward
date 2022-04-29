using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerAttack : MonoBehaviour
{
    [SerializeField, Tooltip("右の攻撃の当たり判定を行うコライダー")]
    Collider2D _rightAttackCol;
    [SerializeField, Tooltip("左の攻撃の当たり判定を行うコライダー")]
    Collider2D _leftAttackCol;
    [SerializeField, Tooltip("上の攻撃の当たり判定を行うコライダー")]
    Collider2D _upAttackCol;
    [SerializeField, Tooltip("下の攻撃の当たり判定を行うコライダー")]
    Collider2D _downAttackCol;

    List<Collider2D> _result = new List<Collider2D>(10);
    ContactFilter2D _filter;

    /// <summary>
    /// 引数のコライダーの範囲内にいる、IDamageのDamage()を呼ぶ関数
    /// </summary>
    public void Attack(float x, float y)
    {
        if(!_downAttackCol || !_leftAttackCol || !_rightAttackCol || !_upAttackCol)
        {
            Debug.Log("PlayerAttackクラスのコライダーがnullです");
            return;
        }

        int count;

        if (x == 1 && y == 0)
        {
            count = _rightAttackCol.OverlapCollider(_filter, _result);
            //_result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
        }
        else if(x == -1 && y == 0)
        {
            count = _leftAttackCol.OverlapCollider(_filter, _result);
            //_result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
        }
        else if(x == 0 && y == 1)
        {
            count = _upAttackCol.OverlapCollider(_filter, _result);
            //_result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
        }
        else if (x == 0 && y == -1)
        {
            count = _downAttackCol.OverlapCollider(_filter, _result);
            //_result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttack : MonoBehaviour
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

    /*
        Attack内のマジックナンバーを変数にしておく 
    */

    /// <summary>
    /// 引数のコライダーの範囲内にいる、IDamageのDamage()を呼ぶ関数
    /// </summary>
    public void Attack(float h, float v)
    {
        if (!_downAttackCol || !_leftAttackCol || !_rightAttackCol || !_upAttackCol)
        {
            Debug.Log("PlayerAttackクラスのコライダーがnullです");
            return;
        }

        int count;

        if (v <= -0.5f && h >= -0.5f || v <= -0.5f && h <= 0.5f)
        {
            count = _downAttackCol.OverlapCollider(_filter, _result);
            _result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
            Debug.Log("down");
        }
        else if (v >= 0.5f && h >= -0.5f || v >= 0.5f && h <= 0.5f)
        {
            count = _upAttackCol.OverlapCollider(_filter, _result);
            _result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
            Debug.Log("up");
        }
        else if (h >= 0.5f && v <= 0.5f || h >= 0.5f && v >= -0.5f)
        {
            count = _rightAttackCol.OverlapCollider(_filter, _result);
            _result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
            Debug.Log("right");
        }
        else if (h <= -0.5f && v <= 0.5f || h <= -0.5f && v >= -0.5f)
        {
            count = _leftAttackCol.OverlapCollider(_filter, _result);
            _result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
            Debug.Log("left");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃範囲のコライダー")]
    Collider2D _attackAria;
    [SerializeField, Tooltip("詳細")]
    ContactFilter2D _filter;

    Collider2D[] _result;
    public void Melee()
    {
        _attackAria.OverlapCollider(_filter, _result);
        Array.ForEach(_result, go => go.GetComponent<PlayerHp>()?.Damage());
    }
}

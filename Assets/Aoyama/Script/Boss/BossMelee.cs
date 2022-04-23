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

    Collider2D[] _result = new Collider2D[5];
    public void Melee()
    {
        _attackAria.OverlapCollider(_filter, _result);
        if (_result == null) return;

        for (var i = 0; i < _result.Length; i++)
        {
            var collider = _result[i];
            collider?.GetComponent<PlayerHP>()?.Damage();    
        }
    }
}

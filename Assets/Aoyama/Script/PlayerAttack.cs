using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃時の当たり判定を行うコライダー")]
    Collider2D _attackCol;

    Collider2D[] _result = new Collider2D[10];

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Attack()
    {
        int count = _attackCol.OverlapCollider(new ContactFilter2D(), _result);
        //_result.Where(go => go.CompareTag("Enemy")).For
    }
}

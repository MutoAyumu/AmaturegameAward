using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Tooltip("�U���̓����蔻����s���R���C�_�[")]
    Collider2D _attackCol;

    List<Collider2D> _result = new List<Collider2D>(10);

    /// <summary>
    /// �����̃R���C�_�[�͈͓̔��ɂ���AIDamage��Damage()���ĂԊ֐�
    /// </summary>
    public void Attack()
    {
        int count = _attackCol.OverlapCollider(new ContactFilter2D(), _result);
        _result.ForEach(go => go.GetComponent<IDamage>()?.Damage());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("�U��������s���R���C�_�[")]
    Collider2D _attackCol;

    ContactFilter2D _filter;
    List<Collider2D> _result = new List<Collider2D>(5);

    int count = 0;
    /// <summary>
    /// Enemy�̍U���p�̊֐�
    /// </summary>
    public void Attack()
    {
        //[ToDo] ContactFilter2D��Serialize���邱�Ƃ�LayerMask���w��ł���̂ŁA�]�T������΂���
        count = _attackCol.OverlapCollider(_filter, _result);
        _result.ForEach(go => go.GetComponent<PlayerHp>()?.Damage());
    }
}

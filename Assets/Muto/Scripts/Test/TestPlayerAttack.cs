using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerAttack : MonoBehaviour
{
    [SerializeField, Tooltip("�E�̍U���̓����蔻����s���R���C�_�[")]
    Collider2D _rightAttackCol;
    [SerializeField, Tooltip("���̍U���̓����蔻����s���R���C�_�[")]
    Collider2D _leftAttackCol;
    [SerializeField, Tooltip("��̍U���̓����蔻����s���R���C�_�[")]
    Collider2D _upAttackCol;
    [SerializeField, Tooltip("���̍U���̓����蔻����s���R���C�_�[")]
    Collider2D _downAttackCol;

    List<Collider2D> _result = new List<Collider2D>(10);
    ContactFilter2D _filter;

    /// <summary>
    /// �����̃R���C�_�[�͈͓̔��ɂ���AIDamage��Damage()���ĂԊ֐�
    /// </summary>
    public void Attack(float x, float y)
    {
        if(!_downAttackCol || !_leftAttackCol || !_rightAttackCol || !_upAttackCol)
        {
            Debug.Log("PlayerAttack�N���X�̃R���C�_�[��null�ł�");
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

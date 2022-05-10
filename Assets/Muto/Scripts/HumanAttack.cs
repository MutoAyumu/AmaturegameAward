using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttack : MonoBehaviour
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

    /*
        Attack���̃}�W�b�N�i���o�[��ϐ��ɂ��Ă��� 
    */

    /// <summary>
    /// �����̃R���C�_�[�͈͓̔��ɂ���AIDamage��Damage()���ĂԊ֐�
    /// </summary>
    public void Attack(float h, float v, int power)
    {
        if (!_downAttackCol || !_leftAttackCol || !_rightAttackCol || !_upAttackCol)
        {
            Debug.Log("PlayerAttack�N���X�̃R���C�_�[��null�ł�");
            return;
        }

        int count;

        if (v <= -0.5f && h >= -0.5f || v <= -0.5f && h <= 0.5f)
        {
            count = _downAttackCol.OverlapCollider(_filter, _result);
            _result.ForEach(go => go.GetComponent<IDamage>()?.Damage(power));
            Debug.Log("down");
            _result.ForEach(go => go.GetComponent<MarblesScript>()?.Hit(Vector2.down));
        }
        else if (v >= 0.5f && h >= -0.5f || v >= 0.5f && h <= 0.5f)
        {
            count = _upAttackCol.OverlapCollider(_filter, _result);
            _result.ForEach(go => go.GetComponent<IDamage>()?.Damage(power));
            Debug.Log("up");
            _result.ForEach(go => go.GetComponent<MarblesScript>()?.Hit(Vector2.up));
        }
        else if (h >= 0.5f && v <= 0.5f || h >= 0.5f && v >= -0.5f)
        {
            count = _rightAttackCol.OverlapCollider(_filter, _result);
            _result.ForEach(go => go.GetComponent<IDamage>()?.Damage(power));
            Debug.Log("right");
            _result.ForEach(go => go.GetComponent<MarblesScript>()?.Hit(Vector2.right));
        }
        else if (h <= -0.5f && v <= 0.5f || h <= -0.5f && v >= -0.5f)
        {
            count = _leftAttackCol.OverlapCollider(_filter, _result);
            _result.ForEach(go => go.GetComponent<IDamage>()?.Damage(power));
            Debug.Log("left");
            _result.ForEach(go => go.GetComponent<MarblesScript>()?.Hit(Vector2.left));
        }

        HitAudio(_result[0]);
    }
    void HitAudio(Collider2D col)
    {
        var hit = col.GetComponent<IDamage>();
        if (hit != null)
        {
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "HumanAttackHit");
        }
    }
}

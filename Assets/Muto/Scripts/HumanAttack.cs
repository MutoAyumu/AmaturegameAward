using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    [SerializeField] CinemachineImpulseSource _source = default;

    [SerializeField] GameObject _hitEffect = default;

    List<Collider2D> _result = new List<Collider2D>(10);
    ContactFilter2D _filter;

    [SerializeField] CriAtomSource _atomSource = default;

    /*
        Attack内のマジックナンバーを変数にしておく 
    */

    /// <summary>
    /// 引数のコライダーの範囲内にいる、IDamageのDamage()を呼ぶ関数
    /// </summary>
    public void Attack(float h, float v, int power)
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

        _source.GenerateImpulse();
        HitAudio(_result);
    }
    void HitAudio(List<Collider2D> col)
    {

        for(int i = 0; i < col.Count; i++)
        {
            var hit = col[i].GetComponent<IDamage>();

            if(hit != null)
            {
                var ef = Instantiate(_hitEffect, col[i].transform.position, Quaternion.identity);
                Destroy(ef,1);
                SoundManager.Instance.CriAtomPlay(CueSheet.SE, "HumanAttackHit");
                return;
            }
        }
    }
}

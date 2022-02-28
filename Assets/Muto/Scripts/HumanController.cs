using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] Transform _ghostMovePos = default;
    [SerializeField] PlayerAttack _attack = default;
    [SerializeField] Animator _anim = default;
    float _lastH;
    float _lastV;
    public Transform GhostMovePos { get => _ghostMovePos; }

    public override void OnUpdate()
    {
        if (_fire1 && _attack)
        {
            _attack.Attack(_lastH, _lastV);
            Debug.Log("攻撃が呼ばれた");
        }
        if (_h != 0 || _v != 0)
        {
            if (_lastH != _h || _lastV != _v)
            {
                _lastH = _h;
                _lastV = _v;
                _anim.SetFloat("X", _lastH);
                _anim.SetFloat("Y", _lastV);
            }
        }

        //※要変更
        Vector2 origin = this.transform.position;
        Debug.DrawLine(origin, origin + new Vector2(_lastH, _lastV), Color.red);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.gameObject.GetComponent<testObjectSearcher>().Search(_lastH, _lastV);
        }
    }
}

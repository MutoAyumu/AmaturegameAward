using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHumanController : TestCharacterControllerBase
{
    [SerializeField] Transform _ghostMovePos = default;
    [SerializeField] PlayerAttack _attack = default;
    [SerializeField] Animator _anim = default;
    [SerializeField] TestMoveTheBlocks _moveTheBlocks = default;
    [SerializeField] testObjectSearcher _searchar = default;
    float _lastH;
    float _lastV;
    bool isSeize;
    public Transform GhostMovePos { get => _ghostMovePos; }
    public bool IsSeize { get => isSeize; set => isSeize = value; }

    /*ToDo
        �l�Ԃɂ��������������Ƃ������ɂ܂Ƃ߂�
    */
    //public override void OnUpdate()
    //{
    //    if (_fire1 && _attack)
    //    {
    //        _attack.Attack(_lastH, _lastV);
    //        Debug.Log("�U�����Ă΂ꂽ");
    //    }
    //    if (_h != 0 || _v != 0)
    //    {
    //        if (_lastH != _h || _lastV != _v)
    //        {
    //            _lastH = _h;
    //            _lastV = _v;
    //            _anim.SetFloat("X", _lastH);
    //            _anim.SetFloat("Y", _lastV);
    //        }
    //    }

    //    //���v�ύX
    //    Vector2 origin = this.transform.position;
    //    Debug.DrawLine(origin, origin + new Vector2(_lastH, _lastV), Color.red);
    //    if(Input.GetButtonDown("Fire2") && !TestCharacterManager._instance.Ghost.IsFollow && !isSeize && TestCharacterManager._instance.Human.IsControll)
    //    {
    //        _moveTheBlocks.Seize(_lastH, _lastV);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Q) && _searchar)
    //    {
    //        _searchar.Search(_lastH, _lastV);
    //    }
    //    else if(Input.GetButton("Fire2") && _moveTheBlocks && !TestCharacterManager._instance.Ghost.IsFollow && isSeize && TestCharacterManager._instance.Human.IsControll)  //���̕ӂ̓��͂Ɋւ��Ă̓x�[�X�̂ق��ŊǗ�����\��
    //    {
    //        _moveTheBlocks.Move(_h, _v);
    //    }

    //    if (Input.GetButtonUp("Fire2") && !TestCharacterManager._instance.Ghost.IsFollow && isSeize && TestCharacterManager._instance.Human.IsControll)
    //    {
    //        _moveTheBlocks.Separate();
    //    }
    //}
}

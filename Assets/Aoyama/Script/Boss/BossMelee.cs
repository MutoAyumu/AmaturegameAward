using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : MonoBehaviour
{
    [SerializeField, Tooltip("�U���͈͂̃R���C�_�[")]
    Collider2D _attackAria;
    [SerializeField, Tooltip("�ڍ�")]
    ContactFilter2D _filter;
    [Header("��")]
    [SerializeField] CriAtomSource _criAtomSource;
    [SerializeField] string _cuename = "MedamaAttack";

    Collider2D[] _result = new Collider2D[5];
    public void Melee()
    {
        _criAtomSource.cueSheet = CueSheet.SE.ToString();
        _criAtomSource.cueName = _cuename;
        _criAtomSource.Play();

        _attackAria.OverlapCollider(_filter, _result);
        if (_result == null) return;

        for (var i = 0; i < _result.Length; i++)
        {
            var collider = _result[i];
            collider?.GetComponent<PlayerHP>()?.Damage();    
        }
    }
}

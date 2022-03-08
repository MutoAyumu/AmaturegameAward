using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : Singleton<BossManager>
{
    [SerializeField, Tooltip("��������{�X�̃v���n�u")]
    GameObject _bossPrefab;

    [SerializeField, Tooltip("�{�X�̐����ꏊ")]
    Transform _bossPos;

    public void StartBossBattle()
    {
        //�^�C�����C���Ă�
    }

    public void InstansBoss()
    {
        if(_bossPrefab && _bossPos)
        {
            Instantiate(_bossPrefab, _bossPos.position, Quaternion.identity);
        }       
    }
}

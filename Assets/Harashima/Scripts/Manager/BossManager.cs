using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : Singleton<BossManager>
{
    [SerializeField, Tooltip("生成するボスのプレハブ")]
    GameObject _bossPrefab;

    [SerializeField, Tooltip("ボスの生成場所")]
    Transform _bossPos;

    public void StartBossBattle()
    {
        //タイムライン呼ぶ
    }

    public void InstansBoss()
    {
        if(_bossPrefab && _bossPos)
        {
            Instantiate(_bossPrefab, _bossPos.position, Quaternion.identity);
        }       
    }
}

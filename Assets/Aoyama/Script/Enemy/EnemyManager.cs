using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyのOnOffを管理するクラス
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField, Tooltip("Stage内のすべてのEnemy")]
    GameObject[] _enemyGrp;

    public void Pause()
    {
        Array.ForEach(_enemyGrp, go => go.GetComponent<EnemyMove>().Pause());
    }

    public void Resume()
    {
        Array.ForEach(_enemyGrp, go => go.GetComponent<EnemyMove>().Resume());
    }
}

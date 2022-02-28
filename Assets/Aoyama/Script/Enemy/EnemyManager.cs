using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyのOnOffを管理するクラス
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField, Tooltip("Enemyをエリアごとに格納する配列")]
    EnemyGroup[] _enemyGroups;

    [SerializeField, Tooltip("現在表示しているEnemyGroup配列のIndex")]
    int _groupIndex = 0;

    //カプセル化
    public int GroupIndex { get => _groupIndex;}

    void Start()
    {
        _groupIndex = 0;

        _enemyGroups[_groupIndex++].OnSetActive();
    }

    /// <summary>
    /// 次のグループを表示する
    /// </summary>
    public void NextGroup()
    {
        _enemyGroups[_groupIndex].OffSetActive();
        _enemyGroups[_groupIndex++].OnSetActive();
    }

    /// <summary>
    /// 前のグループを表示する
    /// </summary>
    public void BackGroup()
    {
        _enemyGroups[_groupIndex].OffSetActive();
        _enemyGroups[_groupIndex--].OnSetActive();
    }

    public void Pause()
    {
        Array.ForEach(_enemyGroups[_groupIndex].EnemyGrp, go => go.GetComponent<EnemyMove>().Pause());
    }

    public void Resume()
    {
        Array.ForEach(_enemyGroups[_groupIndex].EnemyGrp, go => go.GetComponent<EnemyMove>().Resume());
    }
}

[Serializable]
class EnemyGroup
{
    [SerializeField, Tooltip("EnemyGameObjectのグループ")]
    GameObject[] _enemyGrp;

    public GameObject[] EnemyGrp { get; private set; }
    public void OnSetActive()
    {
        Array.ForEach(_enemyGrp, go => go.SetActive(true));
    }

    public void OffSetActive()
    {
        Array.ForEach(_enemyGrp, go => go.SetActive(false));
    }
}

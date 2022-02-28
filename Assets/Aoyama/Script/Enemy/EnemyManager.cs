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

}

[Serializable]
class EnemyGroup
{
    [SerializeField, Tooltip("EnemyGameObjectのグループ")]
    GameObject[] _enemyGroup;

    public void OnSetActive()
    {
        Array.ForEach(_enemyGroup, go => go.SetActive(true));
    }

    public void OffSetActive()
    {
        Array.ForEach(_enemyGroup, go => go.SetActive(false));
    }
}

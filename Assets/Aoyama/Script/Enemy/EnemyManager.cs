using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyのOnOffを管理するクラス
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
    [Tooltip("Stage内のすべてのEnemy")]
    public List<GameObject> Enemys;

    [SerializeField] OnOffEnemy[] _enemyGroup = default;

    public void Pause()
    {
        Enemys?.ForEach(go => go.GetComponent<EnemyMove>().Pause());
    }

    public void Resume()
    {
        Enemys?.ForEach(go => go.GetComponent<EnemyMove>()?.Resume());
    }

    public void SetTarget(Transform tr)
    {
        Enemys?.ForEach(go => go.GetComponent<EnemyMove>()?.SetDecoy(tr));
    }

    public void ResetTarget()
    {
        Enemys?.ForEach(go => go.GetComponent<EnemyMove>()?.ResetDecoy());
    }
    public void DecreaseInNumbers(int i)
    {
        //エネミーグループ配列のi番にある敵の数を減らす
        _enemyGroup[i - 1].Decrease();
    }
}

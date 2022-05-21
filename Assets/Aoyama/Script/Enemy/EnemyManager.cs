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
    List<GameObject> _enemys = new List<GameObject>();

    [SerializeField] OnOffEnemy[] _enemyGroup = default;
    private void Start()
    {
        FieldManager.Instance.OnTextPause += Pause;
        FieldManager.Instance.OnTextResume += Resume;
    }

    public void Pause()
    {
        _enemys?.ForEach(go => go.GetComponent<EnemyMove>().Pause());
    }

    public void Resume()
    {
        _enemys?.ForEach(go => go.GetComponent<EnemyMove>()?.Resume());
    }

    public void SetTarget(Transform tr)
    {
        _enemys?.ForEach(go => go.GetComponent<EnemyMove>()?.SetDecoy(tr));
    }

    public void ResetTarget()
    {
        _enemys?.ForEach(go => go.GetComponent<EnemyMove>()?.ResetDecoy());
    }
    public void DecreaseInNumbers(int i)
    {
        //エネミーグループ配列のi番にある敵の数を減らす
        _enemyGroup[i - 1].Decrease();
    }

    public void AddEnemy(GameObject enemy)
    {
        _enemys.Add(enemy);
    }
}

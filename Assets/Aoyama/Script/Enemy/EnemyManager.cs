using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy��OnOff���Ǘ�����N���X
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField, Tooltip("Stage���̂��ׂĂ�Enemy")]
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

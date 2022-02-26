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
    GameObject[] _enemyGroups;

}

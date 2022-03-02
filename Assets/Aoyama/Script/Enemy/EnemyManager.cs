using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy‚ÌOnOff‚ğŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField, Tooltip("Stage“à‚Ì‚·‚×‚Ä‚ÌEnemy")]
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

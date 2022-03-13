using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy‚ÌOnOff‚ğŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
    [Tooltip("Stage“à‚Ì‚·‚×‚Ä‚ÌEnemy"), System.NonSerialized]
    public List<GameObject> EnemyGrp;

    public void Pause()
    {
        EnemyGrp?.ForEach(go => go.GetComponent<EnemyMove>().Pause());
    }

    public void Resume()
    {
        EnemyGrp?.ForEach(go => go.GetComponent<EnemyMove>()?.Resume());
    }

    public void SetTarget(Transform tr)
    {
        EnemyGrp?.ForEach(go => go.GetComponent<EnemyMove>()?.SetDecoy(tr));
    }

    public void ResetTarget()
    {
        EnemyGrp?.ForEach(go => go.GetComponent<EnemyMove>()?.ResetDecoy());
    }
}

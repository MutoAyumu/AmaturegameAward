using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy��OnOff���Ǘ�����N���X
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
    [Tooltip("Stage���̂��ׂĂ�Enemy")]
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
        //�G�l�~�[�O���[�v�z���i�Ԃɂ���G�̐������炷
        _enemyGroup[i - 1].Decrease();
    }
}

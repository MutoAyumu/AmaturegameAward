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
        //�G�l�~�[�O���[�v�z���i�Ԃɂ���G�̐������炷
        _enemyGroup[i - 1].Decrease();
    }

    public void AddEnemy(GameObject enemy)
    {
        _enemys.Add(enemy);
    }
}

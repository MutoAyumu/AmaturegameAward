using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy��OnOff���Ǘ�����N���X
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField, Tooltip("Enemy���G���A���ƂɊi�[����z��")]
    EnemyGroup[] _enemyGroups;

    [SerializeField, Tooltip("���ݕ\�����Ă���EnemyGroup�z���Index")]
    int _groupIndex = 0;

    //�J�v�Z����
    public int GroupIndex { get => _groupIndex;}

    void Start()
    {
        _groupIndex = 0;

        _enemyGroups[_groupIndex++].OnSetActive();
    }

    /// <summary>
    /// ���̃O���[�v��\������
    /// </summary>
    public void NextGroup()
    {
        _enemyGroups[_groupIndex].OffSetActive();
        _enemyGroups[_groupIndex++].OnSetActive();
    }

    /// <summary>
    /// �O�̃O���[�v��\������
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
    [SerializeField, Tooltip("EnemyGameObject�̃O���[�v")]
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

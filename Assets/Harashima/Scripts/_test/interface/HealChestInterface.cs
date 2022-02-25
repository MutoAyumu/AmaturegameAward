using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealChestInterface : MonoBehaviour, IAction
{
    [SerializeField,Tooltip("�񕜂���HP")]
    int _addHealth = 2;

    [SerializeField, Tooltip("���̃I�u�W�F�N�g�̃A�j���[�^�[�R���|�[�l���g")]
    Animator _animator;

    /// <summary>���̕󔠂����ɊJ���Ă��邩�𔻒肷��t���O</summary>
    bool isOpen = false;

    public void Action()
    {
        if(!isOpen)
        {
            testPlayerStat.Instance.HPfluctuation(_addHealth);
            _animator.SetTrigger("Open");
            isOpen = true;
        }
        
    }
}

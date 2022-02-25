using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class testChestBase : MonoBehaviour
{
    [SerializeField, Tooltip("���̃I�u�W�F�N�g�̃A�j���[�^�[�R���|�[�l���g")]
    protected Animator _animator;

    /// <summary>���̕󔠂����ɊJ���Ă��邩�𔻒肷��t���O</summary>
    bool isOpen = false;
    void Start()
    {
        
    }

    public void Action()
    {
        if (!isOpen)
        {
            OnAction();
        }
    }

    protected abstract void OnAction();
}

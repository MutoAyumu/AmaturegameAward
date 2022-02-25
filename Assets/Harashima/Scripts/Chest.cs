using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IAction
{
    [SerializeField,Tooltip("�h���b�v����A�C�e��")]
    GameObject[] _itemPrefabs;

    [SerializeField, Tooltip("���̃I�u�W�F�N�g�̃A�j���[�^�[�R���|�[�l���g")]
    Animator _animator;

    /// <summary>���̕󔠂����ɊJ���Ă��邩�𔻒肷��t���O</summary>
    bool isOpen = false;

    public void Action()
    {
        if (!isOpen)
        {
            PlayerStat.Instance.AddItem(_itemPrefabs[0]); //�A�C�e���l��
            _animator.SetTrigger("Open");
            isOpen = true; //�J�����̂Ńt���O��True��
        }        
    }
}

/// <summary>
/// �t�B�[���h�I�u�W�F�N�g�̏�������������C���^�[�t�F�[�X
/// </summary>
public interface IAction
{
    void Action();
}

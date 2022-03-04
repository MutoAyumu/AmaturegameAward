using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IActivate
{
    [SerializeField, Tooltip("�h���b�v����A�C�e��")]
    int _dropItemIndex;

    [SerializeField, Tooltip("�h���b�v���鐔")]
    int _dropValue  =1;

    [SerializeField, Tooltip("���̃I�u�W�F�N�g�̃A�j���[�^�[�R���|�[�l���g")]
    Animator _animator;

    /// <summary>���̕󔠂����ɊJ���Ă��邩�𔻒肷��t���O</summary>
    bool isOpen = false;

    public void Action()
    {
        if (!isOpen)
        {
            ItemManager.Instance.ItemValueChange(_dropItemIndex, _dropValue);//�A�C�e���l��
            _animator.SetTrigger("Open");
            isOpen = true; //�J�����̂Ńt���O��True��
        }
    }
}

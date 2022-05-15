using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChestInterface : MonoBehaviour,IActivate,ISetText
{
    [SerializeField,Tooltip("�h���b�v����A�C�e��")]
    GameObject[] _itemPrefabs;

    [SerializeField, Tooltip("���̃I�u�W�F�N�g�̃A�j���[�^�[�R���|�[�l���g")]
    Animator _animator;

    [SerializeField, Tooltip("�A�C�e�����g������SE")]
    AudioClip[] _audios;

    [SerializeField] string _text = "B �󔠂��J����";

    /// <summary>���̕󔠂����ɊJ���Ă��邩�𔻒肷��t���O</summary>
    bool isOpen = false;

    public void Action()
    {
        if (!isOpen)
        {
            TestItemManager.Instance.AddItem(_itemPrefabs[0]); //�A�C�e���l��
            _animator.SetTrigger("Open");
            isOpen = true; //�J�����̂Ńt���O��True��
        }        
    }
    public string SetText()
    {
        return _text;
    }
}

/// <summary>
/// �t�B�[���h�I�u�W�F�N�g�̏�������������C���^�[�t�F�[�X
/// </summary>
public interface IActivate
{
    void Action();
}
public interface ISetText
{ 
    string SetText();
}

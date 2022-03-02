using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    /// <summary> �l���A�C�e�����i�[���郊�X�g</summary>
    GameObject[] _inventry ;
    public GameObject[] Inventry => _inventry;

    GameObject[] _UIinventry = new GameObject[4];
    public GameObject[] UIInventry => _UIinventry;

    [SerializeField, Tooltip("�C���x���g���̏���l�A��{�͂S")]
    const int _inventryLimit = 4;

    GameObject lastTryAddItem;
    public GameObject LastItem => lastTryAddItem;
    /// <summary>
    /// �C���x���g���ɃA�C�e��������֐�
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        lastTryAddItem = item;
        for(int i = 0;i< _inventry.Length;i++)
        {
            if(!_inventry[i])
            {
                //UI��̃C���x���g���ɐ���
                _UIinventry[i] =  Instantiate(item,FieldManager.Instance.InventryPanels[i].transform);
                _inventry[i] = item;
                Debug.Log($"{item}����ɓ��ꂽ");
                return;
            }
        }
        //�C���x���g���������ς��̂Ƃ�
        FieldManager.Instance.ChoiceActive(true);
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < _inventry.Length; i++)
        {
            if (_inventry[i] == item)
            {
                Destroy(_UIinventry[i]); 
                _inventry[i] = null;
                return;
            }
        }
    }

    protected override void OnAwake()
    {
        //�V�[�����؂�ւ���Ă��l���ێ������悤��
        DontDestroyOnLoad(this);

        _inventry = new GameObject[_inventryLimit];
    }

    public void InstanceItem()
    {
        for (int i = 0;i<Inventry.Length;i++)
        {
            if (_inventry[i])
            {
                _UIinventry[i] = Instantiate(_inventry[i], FieldManager.Instance.InventryPanels[i].transform);
            }
        }
    }
}

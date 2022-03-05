using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManager : Singleton<FieldManager>
{

    /// <summary>���U���g���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnClear;

    /// <summary>�Q�[���I�[�o�[���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnGameOver;

    /// <summary>�|�[�Y���ɌĂ΂�郁�\�b�h</summary>
    //public event Action OnPause;

    /// <summary>�ĊJ���ɌĂ΂�郁�\�b�h</summary>
    //public event Action OnResume;

    /// <summary>�N���A���Q�[���I�[�o�[�𔻒肷��t���O</summary>
    bool _isEnd = false;

    [SerializeField,Range(1, 10),Tooltip("�X�e�[�W�̔ԍ�")]
    int _stageIndex;

    void Start()
    {
        //�e�X�g�p
        OnGameOver += DebugGameOver;
        OnClear += DebugClear;

        TestItemManager.Instance?.InstanceItem();
        
    }


    void Update()
    {
        if (PlayerPalam.Instance?.Life <= 0 && !_isEnd)�@//�X�R�A��0�ɂȂ�AisEnd��False��������
        {
            if(OnGameOver != null)
            {
                //�Q�[���I�[�o�[�C�x���g���Ă�
                OnGameOver();
            }            
            _isEnd = true;
        }
        ItemInput();
    }


    public void Clear()
    {
        //�N���A�C�x���g���Ă�
        if(OnClear!= null && !_isEnd)//isEnd��False��������
        {
            OnClear();
            _isEnd = true;
        }

        //�N���A���̏������Ă�
        GameManager.Instance?.ClearStage(_stageIndex);
    }

    [SerializeField, Tooltip("�f�o�b�O�p�̃��U���g�p�l��")]
    GameObject _resultPanel;
    void DebugGameOver()
    {
        _resultPanel.SetActive(true);
        Debug.Log("�Q�[���I�[�o�[");
    }

    void DebugClear()
    {
        _resultPanel.SetActive(true);
        Debug.Log("�N���A");
    }

    //�ȉ��d�l�̎���
    [SerializeField,Tooltip("�A�C�e����\������p�l��")]
    GameObject[] _inventryPanels;
    public GameObject[] InventryPanels => _inventryPanels;

    [SerializeField, Tooltip("�A�C�e����\������C���[�W")]
    GameObject[] _inventryImage;

    [SerializeField,Tooltip("�A�C�e���̌���\������UIText")]
    Text[] _texts;
    public Text[] Texts => _texts;

    public void ChangeTextValue(int index,int value)
    {
        _texts[index].text = value.ToString();
    }

    public void FirstGet(int index)
    {
        _inventryImage[index].SetActive(true);
        _texts[index].gameObject.SetActive(true);
    }

    /// <summary>
    /// �f�o�b�O�p�B�C���v�b�g���󂯎��֐��i���j
    /// </summary>
    void ItemInput()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ItemManager.Instance.UseItem(0);
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            ItemManager.Instance.UseItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ItemManager.Instance.UseItem(2);
        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            ItemManager.Instance.UseItem(3);
        }
    }


    //�ȉ��̓��ł̃A�C�e���̎d�l
    [SerializeField]
    GameObject _inventryButton;

    /// <summary>
    /// ���̊֐�
    /// </summary>
    public void ChoiceActive(bool active)
    {
        _inventryButton.SetActive(active);
    }

    /// <summary>
    /// �f�o�b�O�{�^���p�A�w�肵��Index�̃C���x���g�����폜����
    /// </summary>
    /// <param name="index"></param>
    public void RemoveItem(int index)
    {
        TestItemManager.Instance.RemoveItem(TestItemManager.Instance.Inventry[index]);
        TestItemManager.Instance.AddItem(TestItemManager.Instance.LastItem);
    }
}

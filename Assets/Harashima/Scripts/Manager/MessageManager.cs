using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : Singleton<MessageManager>
{
    [SerializeField, Tooltip("���b�Z�[�W�E�B���h�E�v���n�u")]
    GameObject _messageWindowPrefab;

    /// <summary>���b�Z�[�W�E�B���h�E�̃e�L�X�g�R���|�[�l���g</summary>
    Text _windowText;

    /// <summary>���b�Z�[�W�E�B���h�E�I�u�W�F�N�g </summary>
    GameObject _windowPanel;

    private void Start()
    {
        InstansWindow();
    }

    void InstansWindow()
    {
        _windowText = Instantiate(_messageWindowPrefab,FieldManager.Instance.Canvas.transform).GetComponentInChildren<Text>();
        _windowPanel =  _windowText.transform.parent.gameObject;
    }

    public void SetText(string msg)
    {
        _windowPanel?.SetActive(true);
        if(_windowText)
        {
            _windowText.text = msg;
        }       
    }
}

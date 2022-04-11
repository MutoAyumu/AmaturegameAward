using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : Singleton<MessageManager>
{
    [SerializeField, Tooltip("メッセージウィンドウプレハブ")]
    GameObject _messageWindowPrefab;

    /// <summary>メッセージウィンドウのテキストコンポーネント</summary>
    Text _windowText;

    /// <summary>メッセージウィンドウオブジェクト </summary>
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

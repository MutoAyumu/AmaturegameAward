using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : Singleton<MessageManager>
{
    [SerializeField, Tooltip("���b�Z�[�W�E�B���h�E�v���n�u")]
    GameObject _messageWindowPrefab;

    [SerializeField, Tooltip("���b�Z�[�W�̑��x")]
    float _textSpeed = 0.3f;

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
        ActiveWindow(true);
        if(_windowText && !_isText)
        {
            //_windowText.text = msg;
            StartCoroutine(DrawText(msg));
        }
    }
    IEnumerator DrawText(string text)
    {
        _isText = true;
        float time = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // �N���b�N�����ƈ�C�ɕ\��
            if (IsSpace()) break;

            int len = Mathf.FloorToInt(time / _textSpeed);
            if (len > text.Length) break;
            _windowText.text = text.Substring(0, len);
        }
        _windowText.text = text;
        yield return 0;
        _isText = false;
    }

    public bool IsSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;

        }
        return false;
    }

    public void ActiveWindow(bool active)
    {
        _windowPanel?.SetActive(active);
    }
    //DebugTest
    bool _isText = false;
    private void Update()
    {
        if(!_isText && Input.GetKeyDown(KeyCode.Space))
        {
            ActiveWindow(false);
        }
    }
}
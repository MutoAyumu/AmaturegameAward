using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : Singleton<MessageManager>
{
    [SerializeField, Tooltip("メッセージウィンドウプレハブ")]
    GameObject _messageWindowPrefab;

    [SerializeField, Tooltip("メッセージの速度")]
    float _textSpeed = 0.3f;

    [SerializeField] float _hideTime = 0.5f;

    [SerializeField, Tooltip("一回の上限")]
    int _textLimit = 20;

    [SerializeField] string _skipInputButton = "Jump";

    /// <summary>メッセージウィンドウのテキストコンポーネント</summary>
    Text _windowText;
    Image _windowImage;

    /// <summary>メッセージウィンドウオブジェクト </summary>
    GameObject _windowPanel;

    private void Start()
    {
        InstansWindow();
    }

    void InstansWindow()
    {
        _windowPanel = Instantiate(_messageWindowPrefab, FieldManager.Instance.Canvas.transform);
        var children = _windowPanel.GetComponentsInChildren<RectTransform>();
        _windowText = children[2].GetComponent<Text>();
        _windowImage = children[3].GetComponent<Image>();
    }

    public void SetText(string msg)
    {
        ActiveWindow(true);
        _windowImage.enabled = false; //ここ変えるかも

        if (_windowText && !_isText)
        {
            //_windowText.text = msg;
            StartCoroutine(DrawText(msg));
        }
    }
    public void SetText(string msg, Sprite sprite)
    {
        ActiveWindow(true);
        _windowImage.enabled = true;
        _windowImage.sprite = sprite;

        if (_windowText && !_isText)
        {
            //_windowText.text = msg;
            FieldManager.Instance.TextPause();
            StartCoroutine(DrawText(msg));
        }
    }
    IEnumerator DrawText(string text)
    {
        _isText = true;
        float time = 0;
        while (true)
        {
            yield return null;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsSpace())
            {
                break;
            }

            int len = Mathf.FloorToInt(time / _textSpeed);
            if (len > text.Length) break;
            _windowText.text = text.Substring(0, len);
        }
        _windowText.text = text;
        //yield return 0;
        _isText = false;
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SystemText");
        //StartCoroutine(HideWindows());
        yield return new WaitUntil(()=>IsSpace());
        FieldManager.Instance.TextResume();
        yield break;
    }
    IEnumerator HideWindows()
    {
        yield return new WaitForSeconds(_hideTime);

        if(!_isText && _windowPanel.activeSelf)
        {
            ActiveWindow(false);
            FieldManager.Instance.TextResume();
        }
    }

    public bool IsSpace()
    {
        if (Input.GetButtonDown(_skipInputButton))
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
        if (!_isText && Input.GetButtonDown(_skipInputButton) && _windowPanel.activeSelf)
        {
            ActiveWindow(false);
        }
    }
}

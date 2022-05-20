using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCharacter : MonoBehaviour,IActivate,ISetText
{
    [Tooltip("�\�����郁�b�Z�[�W")]
    string[] _message;
    bool IsFlag = true;

    [SerializeField] string _text = "B �b��������";
    [SerializeField] Sprite _sprite = default;
    public void SetMessage(string[] m)
    {
        _message = m;
    }
    public void IsMessage(bool flag)
    {
        IsFlag = flag;
    }
    public void Action()
    {
        if (IsFlag)//���b�Z�[�W�t���O�������Ă���Ƃ����������悤�ɂ���
        {
            var i = GameManager.Instance.ReturnPoint();
            MessageManager.Instance.SetText(_message[Mathf.Clamp(i, 0, _message.Length - 1)], _sprite);
        }
    }
    public string SetText()
    {
        return _text;
    }
}

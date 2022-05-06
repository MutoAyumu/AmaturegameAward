using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCharacter : MonoBehaviour,IActivate
{
    [Tooltip("表示するメッセージ")]
    string[] _message;
    bool IsFlag = true;
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
        if (IsFlag)//メッセージフラグが立っているときだけ流れるようにする
        {
            var i = GameManager.Instance.ReturnPoint();
            MessageManager.Instance.SetText(_message[Mathf.Clamp(i, 0, _message.Length - 1)]);
        }
    }
}

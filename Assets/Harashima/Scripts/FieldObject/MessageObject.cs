using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageObject : MonoBehaviour,IActivate
{
    [SerializeField, Tooltip("表示するメッセージ")]
    string _message;
    public void Action()
    {
        MessageManager.Instance.SetText(_message);
    }
}

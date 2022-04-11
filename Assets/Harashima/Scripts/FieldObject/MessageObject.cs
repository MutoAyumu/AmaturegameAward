using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageObject : MonoBehaviour,IActivate
{
    [SerializeField, Tooltip("�\�����郁�b�Z�[�W")]
    string _message;
    public void Action()
    {
        MessageManager.Instance.SetText(_message);
    }
}

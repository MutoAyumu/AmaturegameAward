using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCharacter : MonoBehaviour,IActivate
{
    [SerializeField, Tooltip("�\�����郁�b�Z�[�W")]
    string[] _message;
    public void Action()
    {
        var i = GameManager.Instance.ReturnPoint();
        MessageManager.Instance.SetText(_message[Mathf.Clamp(i, 0, _message.Length - 1)]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// OnClick�̓��e���R���g���[���[�œ��I�Ɏ󂯎�邽�߂̂���
/// </summary>
public class ButtonInput : MonoBehaviour
{
    [SerializeField] string _input = "Fire2";
    
    Button _button;

    private void Start()
    {
        if(_button == null)
        {
            _button = GetComponent<Button>();
        }
    }

    void Update()
    {
        if(Input.GetButtonDown(_input))
        {
            _button.onClick.Invoke();
        }
    }
}

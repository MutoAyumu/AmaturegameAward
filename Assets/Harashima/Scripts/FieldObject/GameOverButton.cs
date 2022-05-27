using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverButton : MonoBehaviour
{
    [SerializeField]
    Button _button = null;

    private void Start()
    {
        if (!_button)
        {
            _button = this.GetComponent<Button>();
        }
        _button.onClick.AddListener(Init) ;
    }

    private void Init()
    {
        FieldManager.Instance.OnStartEvent();
        SoundManager.Instance.BGMPlay();
    }
}
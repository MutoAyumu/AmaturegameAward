using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if(FieldManager.Instance.StageIndex %3 !=0)
        {
            _button.onClick.AddListener(Init);
        }
        else
        {
            _button.onClick.AddListener(BossInit);
        }
        
    }

    private void Init()
    {
        FieldManager.Instance.OnStartEvent();
        SoundManager.Instance.BGMPlay();
    }

    void BossInit()
    { 
        SceneManager.LoadSceneAsync($"Scene{FieldManager.Instance.StageIndex}");
    }
}
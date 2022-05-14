using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugButton : MonoBehaviour
{
    [SerializeField] int _num;

    Button _button;
    [SerializeField]
    bool isTitle = false;
    private void Start()
    {
        if(!isTitle)
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(DebugAdd);
        }
        else
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Title);
        }
    }
    void DebugAdd()
    {
        SceneManager.LoadScene("Scene" + _num);
    }

    void Title()
    {
        SceneManager.LoadScene("Title");
    }
}

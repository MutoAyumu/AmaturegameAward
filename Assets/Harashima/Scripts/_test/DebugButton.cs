using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugButton : MonoBehaviour
{
    [SerializeField] int _num;

    Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(DebugAdd);
    }
    void DebugAdd()
    {
        SceneManager.LoadScene("Scene" + _num);
    }
}

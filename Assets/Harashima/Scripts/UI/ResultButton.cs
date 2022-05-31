using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultButton : MonoBehaviour
{
    Button _button = null;
    private void Start()
    {
        _button =GetComponent<Button>();
        _button.onClick.AddListener(PlayerPalam.Instance.ResetLife);
    }
}

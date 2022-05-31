using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStageSelect : MonoBehaviour
{
    [SerializeField] GameObject[] _selectedUiArray;
    private void OnDisable()
    {
        foreach(var i in _selectedUiArray)
        {
            i.gameObject.SetActive(false);
        }
    }
    public void ActiveSelectedUI()
    {
        foreach (var i in _selectedUiArray)
        {
            i.gameObject.SetActive(true);
        }
    }
}

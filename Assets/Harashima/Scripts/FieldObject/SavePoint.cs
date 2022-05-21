using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour,IActivate
{
    int _savePointIndex = 0;
    private void Start()
    {
        _savePointIndex = FieldManager.Instance.AddSavePoint();
    }
    public void Action()
    {
        
    }
}

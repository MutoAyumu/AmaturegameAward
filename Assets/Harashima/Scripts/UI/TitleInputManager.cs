using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TitleInputManager : MonoBehaviour
{
    [SerializeField] UnityEvent _onStartButtonClickEvent = new UnityEvent();

    [SerializeField]
    string _cueName = "SystemSelect";
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            SoundManager.Instance.CriAtomPlay(CueSheet.SE,_cueName);
            _onStartButtonClickEvent?.Invoke();
        }
    }
}

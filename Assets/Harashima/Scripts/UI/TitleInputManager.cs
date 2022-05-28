using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TitleInputManager : MonoBehaviour
{
    [SerializeField] UnityEvent _onStartButtonClickEvent = new UnityEvent();
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            _onStartButtonClickEvent?.Invoke();
        }
    }
}

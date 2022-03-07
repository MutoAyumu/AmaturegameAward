using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour, IGimic
{
    bool _isEnable = false;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void Activate()
    {
        _isEnable = !_isEnable;
        //ここから下にドアを開ける処理を書く
        gameObject.SetActive(_isEnable);
    }
}

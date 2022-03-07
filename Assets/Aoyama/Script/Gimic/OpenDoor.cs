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
        //‚±‚±‚©‚ç‰º‚ÉƒhƒA‚ğŠJ‚¯‚éˆ—‚ğ‘‚­
        gameObject.SetActive(_isEnable);
    }
}

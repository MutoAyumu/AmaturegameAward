using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    int _maxHp = 0;
    PlayerPalam _playerPalam;

    void Start()
    {
        _playerPalam = PlayerPalam.Instance;
        _maxHp = _playerPalam.Life;
    }

    public void Damage()
    {
        _playerPalam.LifeChange(-1);

        if (_playerPalam.Life == 0)
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        Debug.Log("Player‚ª€–S‚µ‚½");

        //Player‚ª€‚ñ‚¾‚Æ‚«‚Ìˆ—‚ğ‘‚­
    }
}

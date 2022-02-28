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

    // Update is called once per frame
    public void Damage()
    {
        _playerPalam.LifeChange(-1);
    }
}

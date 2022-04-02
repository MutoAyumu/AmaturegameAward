using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField, Tooltip("Player‚ªŽ€‚ñ‚¾‚Æ‚«‚ÌƒvƒŒƒnƒu")]
    GameObject _deathPrefab;

    //test
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
        Debug.Log("Player‚ªŽ€–S‚µ‚½");

        Destroy(gameObject);
        if(_deathPrefab)
        {
            var go = Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }
}

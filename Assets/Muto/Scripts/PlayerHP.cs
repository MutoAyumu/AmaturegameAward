using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField, Tooltip("Player‚ªŽ€‚ñ‚¾‚Æ‚«‚ÌƒvƒŒƒnƒu")]
    GameObject _deathPrefab;

    //test
    int _maxHp = 0;
    PlayerPalam _playerPalam;
    CharacterManager _characterManager;

    void Start()
    {
        _playerPalam = PlayerPalam.Instance;
        _characterManager = CharacterManager.Instance;
        _maxHp = _playerPalam.Life;
        _characterManager.UIHPUpdate(_playerPalam.Life);
    }

    public void Damage()
    {
        _playerPalam.LifeChange(-1);
        _characterManager.UIHPUpdate(_playerPalam.Life);

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

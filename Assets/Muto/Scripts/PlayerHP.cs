using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField, Tooltip("Player‚ªŽ€‚ñ‚¾‚Æ‚«‚ÌƒvƒŒƒnƒu")]
    GameObject _deathPrefab;
    [SerializeField] SpriteRenderer _sprite = default;
    [SerializeField] CinemachineImpulseSource _source = default;
    [SerializeField] string _sheetName = "HumanDamage";
    [SerializeField] CharacterControllerBase _character = default;

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
    bool isDead = false;
    public void Damage()
    {
        if(_character.IsDamage || FieldManager.Instance.IsDead)
        {
            return;
        }

        CamShake();
        _character.IsDamageAction();

        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _sheetName);

        if (_characterManager.GodMode) return;

        _playerPalam.LifeChange(-1);
        _characterManager.UIHPUpdate(_playerPalam.Life);

        if (_playerPalam.Life <= 0 && !isDead)
        {
            isDead = true;
            PlayerDeath();
        }
    }
    public void CamShake()
    {
        _source.GenerateImpulse();
    }

    void PlayerDeath()
    {
        Debug.Log("Player‚ªŽ€–S‚µ‚½");

        //Destroy(gameObject);
        _characterManager.CharacterDead();
        if(_deathPrefab)
        {
            var go = Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }
}

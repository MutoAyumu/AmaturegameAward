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
    bool isDamage;

    void Start()
    {
        _playerPalam = PlayerPalam.Instance;
        _characterManager = CharacterManager.Instance;
        _maxHp = _playerPalam.Life;
        _characterManager.UIHPUpdate(_playerPalam.Life);
    }
    private void Update()
    {
        if (isDamage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            _sprite.color = new Color(1f, 1f, 1f, level);
        }
    }

    public void Damage()
    {
        if(isDamage || FieldManager.Instance.IsDead)
        {
            return;
        }

        CamShake();
        DamageAnim();

        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _sheetName);

        if (_characterManager.GodMode) return;

        _playerPalam.LifeChange(-1);
        _characterManager.UIHPUpdate(_playerPalam.Life);

        if (_playerPalam.Life <= 0)
        {
            PlayerDeath();
        }
    }
    public void CamShake()
    {
        if(isDamage)
        {
            return;
        }

        _source.GenerateImpulse();
    }

    public void DamageAnim()
    {
        if(isDamage)
        {
            return;
        }

        var a = _sprite.color.a;
        _character.IsDamage();
        StartCoroutine(OnDamage(a));

        isDamage = true;
    }

    IEnumerator OnDamage(float alpha)
    {
        yield return new WaitForSeconds(3.0f);

        isDamage = false;
        _sprite.color = new Color(1, 1, 1, alpha);
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

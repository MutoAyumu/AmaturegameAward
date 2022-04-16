using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField, Tooltip("Player‚ªŽ€‚ñ‚¾‚Æ‚«‚ÌƒvƒŒƒnƒu")]
    GameObject _deathPrefab;
    [SerializeField] SpriteRenderer _sprite = default;

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
        if(isDamage)
        {
            return;
        }

        DamageAnim();

        _playerPalam.LifeChange(-1);
        _characterManager.UIHPUpdate(_playerPalam.Life);

        if (_playerPalam.Life == 0)
        {
            PlayerDeath();
        }


    }

    public void DamageAnim()
    {
        if(isDamage)
        {
            return;
        }

        StartCoroutine(OnDamage());

        isDamage = true;
    }

    IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(3.0f);

        isDamage = false;
        _sprite.color = new Color(1, 1, 1, 1);
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

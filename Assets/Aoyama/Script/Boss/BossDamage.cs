using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour, IDamage
{
    [Header("�e��X�e�[�^�X")]
    [SerializeField, Tooltip("Enemy��HP")]
    int _enemyHp = 30;
    [SerializeField]
    Slider _enemyHpSlider;
    [SerializeField] bool death = false;
    [SerializeField] bool _damage = false;

    [Header("���񂾂Ƃ�")]
    [SerializeField] bool _timeLineCall = true;
    [SerializeField, Tooltip("���񂾂Ƃ��̃v���n�u")]
    GameObject _deathPrefab;
    [SerializeField] UnityEvent _deathEvent;

    [Header("�Ƃ肠�����Q�Ƃ��������")]
    [SerializeField] Animator _anim;
    [SerializeField] EnemyDamageText _enemyDamageText = default;
    [SerializeField] GameObject _destroy;
 
    void Start()
    {
        _enemyHpSlider.maxValue = _enemyHp;
        _enemyHpSlider.value = _enemyHp;
    }

    void Update()
    {
        if (death)
        {
            EnemyDeath();
        }

        if(_damage)
        {
            _anim.SetTrigger("Damage");
            _damage = false;
        }
    }

    /// <summary>
    /// �Ăяo���ƃ_���[�W��^����
    /// </summary>
    public void Damage(int damage)
    {
        Debug.Log($"{gameObject.name}�Ƀ_���[�W��^����");

        _enemyDamageText?.DamageText(damage);
        _enemyHp -= damage;
        _enemyHpSlider.value = _enemyHp;

        if(_anim)
        {
            _anim.SetTrigger("Damage");
        }

        if (_enemyHp <= 0)
        {
            EnemyDeath();
        }
    }

    /// <summary>
    /// HP���[���ɂȂ�ƌĂ΂��
    /// ���ʂƂ��̏���
    /// </summary>
    void EnemyDeath()
    {
        SoundManager.Instance.CriAtomStop();
        _destroy.GetComponent<OnOffEnemy>().Decrease();
        Debug.Log("EnemyDeath���Ăяo���ꂽ");
        Destroy(gameObject);
        Destroy(_destroy, 2f);

        if(_timeLineCall)
        {
            _deathEvent.Invoke();
        }
        else
        {
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
        
    }

}

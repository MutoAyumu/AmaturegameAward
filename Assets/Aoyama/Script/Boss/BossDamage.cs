using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour, IDamage
{
    [Header("�e��X�e�[�^�X")]
    [SerializeField, Tooltip("Enemy��HP")]
    int _enemyHp = 30;
    [SerializeField]
    Slider _enemyHpSlider;
    [SerializeField] bool death = false;

    [Header("GameObject")]
    [SerializeField, Tooltip("���񂾂Ƃ��̃v���n�u")]
    GameObject _deathPrefab;

    void Start()
    {
        _enemyHpSlider.maxValue = _enemyHp;
        _enemyHpSlider.value = _enemyHp;
    }

    void Update()
    {
        if(death)
        {
            EnemyDeath();
        }
    }

    /// <summary>
    /// �Ăяo���ƃ_���[�W��^����
    /// </summary>
    public void Damage()
    {
        Debug.Log($"{gameObject.name}�Ƀ_���[�W��^����");

        _enemyHpSlider.value = --_enemyHp;

        if (_enemyHp == 0)
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
        Debug.Log("EnemyDeath���Ăяo���ꂽ");
        Destroy(gameObject);

        if (_deathPrefab)
        {
            var go = Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }
}

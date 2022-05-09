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
    [SerializeField] bool _damage = false;

    [Header("GameObject")]
    [SerializeField, Tooltip("���񂾂Ƃ��̃v���n�u")]
    GameObject _deathPrefab;

    [Header("�Ƃ肠�����Q�Ƃ��������")]
    [SerializeField] Animator _anim;

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
        Debug.Log("EnemyDeath���Ăяo���ꂽ");
        Destroy(gameObject);

        if (_deathPrefab)
        {
            var go = Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }

}

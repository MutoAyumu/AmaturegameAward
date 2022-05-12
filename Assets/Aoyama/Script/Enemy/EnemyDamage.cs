using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enemy��HP�֌W���Ǘ�����N���X
/// </summary>
public class EnemyDamage : MonoBehaviour, IDamage
{
    [Header("�e��X�e�[�^�X")]
    [SerializeField, Tooltip("Enemy��HP")]
    int _enemyHp = 2;

    [Header("GameObject")]
    [SerializeField, Tooltip("���񂾂Ƃ��̃v���n�u")]
    GameObject _deathPrefab;

    [SerializeField, Tooltip("AudioClip")]
    AudioClip _audio;

    [Header("�Ƃ肠�����Q�Ƃ��������")]
    [SerializeField] EnemyMove _enemyMove;
    [SerializeField] Animator _anim;
    [SerializeField] bool _testDeath = false;
    [SerializeField] bool _testDamage = false;
    [SerializeField] Text _damageText;

    int _groupNumber;

    private void Update()
    {
        if (_testDeath)
        {
            EnemyDeath();
        }

        //if (_testDamage)
        //{
        //    Damage();
        //    _testDamage = false;
        //}
    }

    /// <summary>
    /// �Ăяo���ƃ_���[�W��^����
    /// </summary>
    public void Damage(int damage)
    {
        Debug.Log($"{gameObject.name}�Ƀ_���[�W��^����");
        _anim.SetTrigger("Damage");
        _enemyHp -= damage; ;
        _enemyMove.KnockBack();

        //�_���[�W��\��      
        if (_damageText)
        {
            _damageText.gameObject.SetActive(true);
            _damageText.text = damage.ToString();
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
        if(_audio)
        {
            SoundManager.Instance.SoundPlay(_audio);
        }

        if (_groupNumber != 0)
        {
            EnemyManager.Instance.DecreaseInNumbers(_groupNumber);
        }

        Debug.Log("EnemyDeath���Ăяo���ꂽ");
        Destroy(gameObject);

        if (_deathPrefab)
        {
            var go = Instantiate(_deathPrefab, transform.position, Quaternion.identity);
            SpriteRenderer sp = go.GetComponent<SpriteRenderer>();
            sp.flipX = this.GetComponent<SpriteRenderer>().flipX;
        }
    }

    public void SetNumber(int i)
    {
        _groupNumber = i;
    }
}

/// <summary>
/// �G��󂹂�A�C�e�����v���C���[����̍U�����󂯂����p��interface
/// </summary>
interface IDamage
{
    void Damage(int damage);
}

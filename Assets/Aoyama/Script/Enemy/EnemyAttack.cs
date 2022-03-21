using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("�U��������s���R���C�_�[")]
    Collider2D _attackCol;
    [SerializeField, Tooltip("�U������܂ł̎���")]
    float _attackTime = 1;
    [SerializeField, Tooltip("AudioClip")]
    AudioClip _audio;
    [Header("�Ƃ肠�����Q�Ƃ��������")]
    [SerializeField]
    Rigidbody2D _rb;
    [SerializeField]
    Animator _anim;

    ContactFilter2D _filter;
    List<Collider2D> _result = new List<Collider2D>(5);

    int _count = 0;
    float _timer = 0;

    Vector2 _dir = new Vector2(0, 0);

    void Update()
    {
        //Player�̋߂��ɂ����ԂŁA_attackTime�̎��Ԃ����ƍU������
        if(_rb.velocity == _dir)
        {
            _timer += Time.deltaTime;
            if(_attackTime <= _timer)
            {
                Attack();
                _timer = 0;
            }
        }
        else
        {
            _timer = 0;
        }
    }

    /// <summary>
    /// Enemy�̍U���p�̊֐�
    /// </summary>
    public void Attack()
    {
        if(_audio)
        {
            SoundManager.Instance.SoundPlay(_audio);
        }

        if(_anim)
        {
            _anim.SetTrigger("Attack");
            Debug.Log($"{gameObject.name}�̍U��");
        }

        //[ToDo] ContactFilter2D��Serialize���邱�Ƃ�LayerMask���w��ł���̂ŁA�]�T������΂���
        _count = _attackCol.OverlapCollider(_filter, _result);
        _result.ForEach(go => go.GetComponent<PlayerHp>()?.Damage());
    }
}

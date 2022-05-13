using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///���u����o��e�̊��R���g���[���[�N���X
/// </summary>
public abstract class EnemyBulletControllerBase : MonoBehaviour
{
    [Header("���̃I�u�W�F�N�g�̃R���|�[�l���g")]
    [SerializeField, Tooltip("RigidBody")]
    protected Rigidbody2D _rb = default;


    [Header("�p�����[�^")]
    [SerializeField, Tooltip("�e�̃X�s�[�h")]
    protected float _speed = 4f;

    private void Update()
    {
        Move();
    }

    public abstract void Move();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }      
    }
}

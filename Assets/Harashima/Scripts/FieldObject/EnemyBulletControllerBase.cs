using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///モブから出る弾の基底コントローラークラス
/// </summary>
public abstract class EnemyBulletControllerBase : MonoBehaviour
{
    [Header("このオブジェクトのコンポーネント")]
    [SerializeField, Tooltip("RigidBody")]
    protected Rigidbody2D _rb = default;


    [Header("パラメータ")]
    [SerializeField, Tooltip("弾のスピード")]
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] int _damage = 10;
    [SerializeField] Rigidbody2D _rb = default;

    private void Start()
    {
        _rb.velocity = this.transform.up * _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamage>()?.Damage(_damage);
            Destroy(this.gameObject, 0.2f); ;
        }
    }
}

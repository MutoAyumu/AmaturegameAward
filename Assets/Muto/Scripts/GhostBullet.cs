using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] Rigidbody2D _rb = default;

    private void Start()
    {
        _rb.velocity = this.transform.up * _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(!collision.CompareTag("Player"))
        //Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblesScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb = default;
    [SerializeField] float _speed = 2f;

    string _playerTag = "Player";

    private void Start()
    {
        _rb.velocity = this.transform.up * _speed;
    }
    public void Hit(Vector2 dir)
    {
        _rb.velocity = dir.normalized * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var go = collision.gameObject.GetComponent<ObstacleSwitch>();

        if (go)
        {
            go?.Action();
        }

        if (!collision.gameObject.CompareTag(_playerTag))
        {
            Destroy(this.gameObject);
        }
    }
}

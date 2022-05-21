using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    [SerializeField] GhostBullet _bullet = default;
    public void Attack(float h, float v)
    {
        var dir = new Vector2(h, v).normalized;

        var go = Instantiate(_bullet, this.transform.position, Quaternion.identity);
        go.transform.up = dir;
    }
}

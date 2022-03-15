using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    [SerializeField] GhostBullet _bullet = default;
    public void Attack()
    {
        Instantiate(_bullet, this.transform.position, Quaternion.identity);
    }
}

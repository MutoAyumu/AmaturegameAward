using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingBullet : EnemyBulletControllerBase
{
    public override void Move()
    {
        Vector3 target = CharacterManager.Instance.PlayerPosition(this.transform);
        
        _rb.velocity = (target - transform.position).normalized * _speed;
    }
}

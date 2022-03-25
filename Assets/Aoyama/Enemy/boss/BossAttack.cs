using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] IAttack[] _attack;

    int _attackIndex = 0;

    void Start()
    {
        
    }

    public void RamdomAttack()
    {
        _attackIndex = Random.Range(0, _attack.Length - 1);
        _attack[_attackIndex].Attack();
    }
}

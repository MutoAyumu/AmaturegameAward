using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] float _attackTime = 5;
    [SerializeField] int _firstIndex = 1;
    [SerializeField] Animator _anim;
    //[SerializeField] aa a;

    int _attackIndex = 0;
    int _beforeIndex = 0;
    float _timer = 0;
    bool _isTimer = false;

    void Start()
    {
        _isTimer = true; Debug.Log(_isTimer);
    }

    void Update()
    {
        if (_isTimer == true)
        {
            Timer();
        }
        
        if(_timer >= _attackTime)
        {
            RamdomAttack();
        }
    }

    public void RamdomAttack()
    {
        _attackIndex = Random.Range(1, 4);
        if (_attackIndex == _beforeIndex)
        {
            RamdomAttack();
            return;
        }

        switch ( _attackIndex)
        {
            case 1:
                _anim.SetTrigger("Attack1");
                break;
            case 2:
                _anim.SetTrigger("Attack2");
                break;
            case 3:
                _anim.SetTrigger("Attack3");
                break;
        }

        _beforeIndex = _attackIndex;
        _isTimer = false;
        _timer = 0;   
    }

    public void AttackComplete()
    {
        _isTimer = true;
    }

    void Timer()
    {
        _timer += Time.deltaTime;
    }
}

//class aa
//{
//    [SerializeField] int a = 0;
//    [SerializeField] GameObject go;
//}

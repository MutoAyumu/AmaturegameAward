using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealChestInterface : MonoBehaviour, IAction
{
    [SerializeField,Tooltip("回復するHP")]
    int _addHealth = 2;

    [SerializeField, Tooltip("このオブジェクトのアニメーターコンポーネント")]
    Animator _animator;

    /// <summary>この宝箱が既に開いているかを判定するフラグ</summary>
    bool isOpen = false;

    public void Action()
    {
        if(!isOpen)
        {
            testPlayerStat.Instance.HPfluctuation(_addHealth);
            _animator.SetTrigger("Open");
            isOpen = true;
        }
        
    }
}

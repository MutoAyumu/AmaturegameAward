using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class testChestBase : MonoBehaviour
{
    [SerializeField, Tooltip("このオブジェクトのアニメーターコンポーネント")]
    protected Animator _animator;

    /// <summary>この宝箱が既に開いているかを判定するフラグ</summary>
    bool isOpen = false;
    void Start()
    {
        
    }

    public void Action()
    {
        if (!isOpen)
        {
            OnAction();
        }
    }

    protected abstract void OnAction();
}

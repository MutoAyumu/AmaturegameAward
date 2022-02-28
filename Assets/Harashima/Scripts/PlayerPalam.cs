using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPalam : Singleton<PlayerPalam>
{
    [SerializeField, Tooltip("ライフの初期値")]
    int _initialLife = 3;

    /// <summary>現在のライフ</summary>
    int _life;
    /// <summary>ライフの読み取りプロパティ</summary>
    public int Life => _life;

    /// <summary>
    /// 現在のライフを変更する関数
    /// </summary>
    /// <param name="value">増やすなら正、減らすなら負</param>
    public void LifeChange(int value)
    {
        Debug.Log($"変化前：{_life}");
        if (_life + value <= 0)
        {
            _life = 0;
        }
        else
        {
            _life += value;
        }
        Debug.Log($"変化後：{_life}");
    }

    protected override void OnAwake()
    {
        //シーンが切り替わっても値が保持されるように
        DontDestroyOnLoad(this);

        //ライフを初期化
        _life = _initialLife;
    }

}

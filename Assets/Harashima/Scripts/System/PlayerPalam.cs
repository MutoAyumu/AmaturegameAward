using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのステータスを管理するクラス
/// </summary>
public class PlayerPalam : Singleton<PlayerPalam>
{
    [Header("ライフ")]
    [SerializeField, Range(1, 10), Tooltip("ライフの初期値")]
    int _initialLife = 3;
    [SerializeField, Range(1, 10), Tooltip("ライフの最大値")]
    int _lifeLimit = 5;

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
        int last = _life;
        _life = Mathf.Clamp(_life+ value,0,_lifeLimit);
        Debug.Log($"変化前：{last}　変化後：{_life}");
    }

    private void Start()
    {
        FieldManager.Instance.OnStart += ResetLife;
    }

    protected override void OnAwake()
    {
        //シーンが切り替わっても値が保持されるように
        DontDestroyOnLoad(this);

        //ライフを初期化
        _life = _initialLife;
    }

    void ResetLife()
    {
        _life = _initialLife;
    }

}

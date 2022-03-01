using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : Singleton<FieldManager>
{
    /// <summary>スタート時に呼ばれるメソッド</summary>
    public event Action OnStart;

    /// <summary>リザルト時に呼ばれるメソッド</summary>
    public event Action OnClear;

    /// <summary>ゲームオーバー時に呼ばれるメソッド</summary>
    public event Action OnGameOver;

    /// <summary>ポーズ時に呼ばれるメソッド</summary>
    public event Action OnPause;

    /// <summary>再開時に呼ばれるメソッド</summary>
    public event Action OnResume;

    /// <summary>クリアかゲームオーバーを判定するフラグ</summary>
    bool _isEnd = false;

    [SerializeField,Range(1, 10),Tooltip("ステージの番号")]
    int _stageIndex;

    void Start()
    {
        //テスト用
        OnGameOver += DebugGameOver;
        OnStart += DebugStart;
        OnClear += DebugClear;
        //スタートイベントを呼ぶ
        if (OnStart != null)
        {
            OnStart();
        }        
    }


    void Update()
    {
        if (PlayerPalam.Instance?.Life <= 0 && !_isEnd)　//スコアが0になり、isEndがFalseだったら
        {
            if(OnGameOver != null)
            {
                //ゲームオーバーイベントを呼ぶ
                OnGameOver();
            }            
            _isEnd = true;
        }
    }


    public void Clear()
    {
        //クリアイベントを呼ぶ
        if(OnClear!= null && !_isEnd)//isEndがFalseだったら
        {
            OnClear();
            _isEnd = true;
        }

        //クリア時の処理を呼ぶ
        GameManager.Instance?.ClearStage(_stageIndex);
    }

    [SerializeField, Tooltip("デバッグ用のリザルトパネル")]
    GameObject _resultPanel;
    void DebugGameOver()
    {
        _resultPanel.SetActive(true);
        Debug.Log("ゲームオーバー");
    }
    void DebugStart()
    {        
        Debug.Log("スタート");
    }
    void DebugClear()
    {
        _resultPanel.SetActive(true);
        Debug.Log("クリア");
    }
    [SerializeField]
    GameObject[] _inventryPanels;
    public GameObject[] InventryPanels => _inventryPanels;

    [SerializeField]
    GameObject _inventryButton;

    /// <summary>
    /// 仮の関数
    /// </summary>
    public void ChoiceActive(bool active)
    {
        _inventryButton.SetActive(active);
    }

    /// <summary>
    /// デバッグボタン用、指定したIndexのインベントリを削除する
    /// </summary>
    /// <param name="index"></param>
    public void RemoveItem(int index)
    {
        ItemManager.Instance.RemoveItem(ItemManager.Instance.Inventry[index]);
        ItemManager.Instance.AddItem(ItemManager.Instance.LastItem);
    }
}

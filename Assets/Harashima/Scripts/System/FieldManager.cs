using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManager : Singleton<FieldManager>
{

    /// <summary>リザルト時に呼ばれるメソッド</summary>
    public event Action OnClear;

    /// <summary>ゲームオーバー時に呼ばれるメソッド</summary>
    public event Action OnGameOver;

    /// <summary>ポーズ時に呼ばれるメソッド</summary>
    //public event Action OnPause;

    /// <summary>再開時に呼ばれるメソッド</summary>
    //public event Action OnResume;

    /// <summary>クリアかゲームオーバーを判定するフラグ</summary>
    bool _isEnd = false;

    [SerializeField,Range(1, 10),Tooltip("ステージの番号")]
    int _stageIndex;

    void Start()
    {
        //テスト用
        OnGameOver += DebugGameOver;
        OnClear += DebugClear;

        TestItemManager.Instance?.InstanceItem();
        
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
        ItemInput();
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

    void DebugClear()
    {
        _resultPanel.SetActive(true);
        Debug.Log("クリア");
    }

    //以下仕様の試作
    [SerializeField,Tooltip("アイテムを表示するパネル")]
    GameObject[] _inventryPanels;
    public GameObject[] InventryPanels => _inventryPanels;

    [SerializeField, Tooltip("アイテムを表示するイメージ")]
    GameObject[] _inventryImage;

    [SerializeField,Tooltip("アイテムの個数を表示するUIText")]
    Text[] _texts;
    public Text[] Texts => _texts;

    public void ChangeTextValue(int index,int value)
    {
        _texts[index].text = value.ToString();
    }

    public void FirstGet(int index)
    {
        _inventryImage[index].SetActive(true);
        _texts[index].gameObject.SetActive(true);
    }

    /// <summary>
    /// デバッグ用。インプットを受け取る関数（仮）
    /// </summary>
    void ItemInput()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ItemManager.Instance.UseItem(0);
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            ItemManager.Instance.UseItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ItemManager.Instance.UseItem(2);
        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            ItemManager.Instance.UseItem(3);
        }
    }


    //以下はα版のアイテムの仕様
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
        TestItemManager.Instance.RemoveItem(TestItemManager.Instance.Inventry[index]);
        TestItemManager.Instance.AddItem(TestItemManager.Instance.LastItem);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// フィールド上での進行を管理するクラス（デバッグ時点ではUIManagerも内包している）
/// </summary>
public class FieldManager : Singleton<FieldManager>
{

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

    [SerializeField, Range(1, 10), Tooltip("ステージの番号")]
    int _stageIndex;

    [SerializeField, Tooltip("フィールドBGM")]
    AudioClip _bgm;

    [SerializeField, Tooltip("シーン上のキャンバス")]
    GameObject _canvas;

    [SerializeField] PlayableDirector _timeLine = default;

    [SerializeField] string _pauseInputName = "Cancel";
    bool IsPause;
    [SerializeField] Image _pausePanel = default;
    /// <summary>シーン上キャンバスの読み取りプロパティ</summary>
    public GameObject Canvas => _canvas;

    protected override void OnAwake()
    {
        if (!_canvas)
        {
            _canvas = FindObjectOfType<Canvas>().gameObject;
        }
    }

    void Start()
    {
        //テスト用
        OnGameOver += DebugGameOver;
        OnClear += DebugClear;
        SoundManager.Instance.BGMPlay();
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

        if(Input.GetButtonDown(_pauseInputName))
        {
            if(OnPause != null && !IsPause)
            {
                IsPause = true;
                OnPause();
            }
            else if(OnResume != null && IsPause)
            {
                IsPause = false;
                OnResume();
            }
            _pausePanel.gameObject.SetActive(IsPause);
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

    void DebugClear()
    {
        //_resultPanel.SetActive(true);
        //_timeLine.Play();
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

    float _timer = 1f;
    [SerializeField]
    float _timeInterval = 1f;
    /// <summary>
    /// デバッグ用。インプットを受け取る関数（仮）
    /// </summary>
    void ItemInput()
    {
        _timer += Time.deltaTime;
        float h = Input.GetAxis("Debug Horizontal");
        float v = Input.GetAxis("Debug Vertical");
        if(_timer > _timeInterval)
        {
            if (Input.GetKeyDown(KeyCode.Z) || h < 0)
            {
                ItemManager.Instance.UseItem(0);
                _timer = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.X) || h > 0)
            {
                ItemManager.Instance.UseItem(1);
                _timer = 0f;
            }
            //else if (Input.GetKeyDown(KeyCode.C) || v < 0)
            //{
            //    ItemManager.Instance.UseItem(2);
            //    _timer = 0f;
            //}
            //else if (Input.GetKeyDown(KeyCode.V) || h < 0)
            //{
            //    ItemManager.Instance.UseItem(3);
            //    _timer = 0f;
            //}            
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

    public void Test()
    {
        Debug.Log("テスト");

        if (OnPause != null && !IsPause)
        {
            IsPause = true;
            OnPause();
        }
        else if (OnResume != null && IsPause)
        {
            IsPause = false;
            OnResume();
        }
    }
}

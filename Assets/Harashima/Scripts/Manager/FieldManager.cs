using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

/// <summary>
/// フィールド上での進行を管理するクラス（デバッグ時点ではUIManagerも内包している）
/// </summary>
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

    public event Action OnTextPause;
    public event Action OnTextResume;

    /// <summary>クリアかゲームオーバーを判定するフラグ</summary>
    bool _isDead = false;

    [SerializeField, Range(1, 10), Tooltip("ステージの番号")]
    int _stageIndex;
    public int StageIndex => _stageIndex;

    [SerializeField, Tooltip("フィールドBGM")]
    AudioClip _bgm;

    [SerializeField, Tooltip("シーン上のキャンバス")]
    GameObject _canvas;

    [Header("ギミック作動時に使われる物")]
    [SerializeField, Tooltip("ギミックが作動した時に使うカメラ")] CinemachineVirtualCamera _eventCam = default;
    [SerializeField] GameObject _eventPanel = default;
    [SerializeField] GameObject _playerCanvas = default;

    [SerializeField] string _pauseInputName = "Cancel";
    bool IsPause;
    [SerializeField] Image _pausePanel = default;
    /// <summary>シーン上キャンバスの読み取りプロパティ</summary>
    public GameObject Canvas => _canvas;
    public bool IsDead => _isDead;

    public void CurrentSavePoint()
    {

    }

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
        SoundManager.Instance.FadeOutAudio(1);
        TestItemManager.Instance?.InstanceItem();
        ItemManager.Instance.SetPanel();
        //OnStart();
    }


    void Update()
    {

        if (PlayerPalam.Instance?.Life <= 0 && !_isDead)　//スコアが0になり、isEndがFalseだったら
        {
            if(OnGameOver != null)
            {
                //ゲームオーバーイベントを呼ぶ
                OnGameOver();
            }            
            _isDead = true;
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
        if(OnClear!= null && !_isDead)//isEndがFalseだったら
        {
            OnClear();
            _isDead = true;
        }

        //クリア時の処理を呼ぶ
        GameManager.Instance?.ClearStage(_stageIndex);
    }

    [SerializeField, Tooltip("デバッグ用のリザルトパネル")]
    GameObject _resultPanel;
    [SerializeField]
    GameObject _textButton;
    [SerializeField]
    float _fadeTime = 3f;
    void DebugGameOver()
    {
        _isDead = false;
        _resultPanel.SetActive(true);
        _resultPanel.GetComponent<Image>().DOFade(0.8f, _fadeTime)
        .OnComplete(() => 
        {
            _textButton.SetActive(true);
        });
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

    public void SetEventCamera(Transform target)
    {
        if(!_eventCam || !_eventPanel)
        {
            Debug.LogError("EventCameraかEventPanelがセットされていません");
            return;
        }

        _eventCam.Follow = target;
        _eventCam.Priority = 20;
        _eventPanel.SetActive(true);
        _playerCanvas.SetActive(false);

        StartCoroutine(CameraReset(target));
    }
    IEnumerator CameraReset(Transform target)
    {
        while(true)
        {
            yield return 0;

            if((Vector2)_eventCam.transform.position == (Vector2)target.position)
            {
                break;
            }
        }

        yield return new WaitForSeconds(1.25f);
        _eventPanel.SetActive(false);
        _playerCanvas.SetActive(true);
        _eventCam.Priority = 0;
    }

    public void TextPause()
    {
        OnTextPause();
    }
    public void TextResume()
    {
        OnTextResume();
    }


    [SerializeField] Image _image = null;
    public void OnStartEvent()
    {
        _image.DOFade(1f, 3f)
            .OnComplete(() => {
                _image.DOFade(0f, 2f);
                 PlayerPalam.Instance.ResetLife();
                _isDead = false;
                _resultPanel.SetActive(false);
                OnStart(); ; });

    }
}

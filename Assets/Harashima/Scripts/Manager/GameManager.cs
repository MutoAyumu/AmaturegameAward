using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Range(1, 10), Tooltip("何ステージあるか")]
    int _stageLimit;
    public int StageLimit => _stageLimit;

    [Header("デバッグ")]
    [SerializeField]
    bool _isDebug = false;
    [SerializeField]GameObject _debugPanelPrefab = default;
    GameObject _debugPanel = default;
    [SerializeField] GameObject _debugtextPrefab = default;
    GameObject _debugText = default;

    /// <summary>現在のステージクリア状況</summary>
   [SerializeField]
    bool[] _clearedStage;
    public bool[] ClearedStage => _clearedStage;

    public int _friendShipPoints;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
        //ステージの数で初期化
        _clearedStage = new bool[_stageLimit];
        _clearedStage[0] = true;
    }

    void Start()
    {
        //デバッグ用のパネルを生成
        _debugPanel = Instantiate(_debugPanelPrefab, this.transform);
        _debugPanel.SetActive(false);

        if(_debugtextPrefab)
        {
            _debugText =Instantiate(_debugtextPrefab,this.transform);
            _debugText.SetActive(false);
        }

        SceneManager.sceneLoaded += DebugPanelActiveFalse;
  

    }


    /// <summary>
    /// ステージをクリアした際の処理を行う関数
    /// </summary>
    /// <param name="index">1以上のステージ数以下の値</param>
    public void ClearStage(int index)
    {
        int num = index - 1;
        num = Mathf.Clamp(num, 0, _stageLimit - 1);
        _clearedStage[num] = true;
    }
    /// <summary>
    /// 友好ポイントを返す関数
    /// </summary>
    /// <returns></returns>
    public int ReturnPoint()
    {
        return Mathf.Clamp(_friendShipPoints, 0, 30);
    }

    private void Update()
    {
        if (_isDebug)
        {
            _debugText?.SetActive(true);
            if (Input.GetKeyDown(KeyCode.G))
            {
                _debugPanel.SetActive(true);
            }
        }
    }

    void DebugPanelActiveFalse(Scene scene, LoadSceneMode mode)
    {
        _debugPanel.SetActive(false);
    }
}

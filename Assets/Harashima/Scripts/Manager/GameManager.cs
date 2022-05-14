using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Range(1, 10), Tooltip("何ステージあるか")]
    int _stageLimit;

    [Header("デバッグ")]
    [SerializeField]
    bool _isDebug = false;
    [SerializeField]
    GameObject _debugPanelPrefab = default;
    GameObject _debugPanel = default;

    /// <summary>現在のステージクリア状況</summary>
    bool[] _clearedStage;

    public int _friendShipPoints;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        //ステージの数で初期化
        _clearedStage = new bool[_stageLimit];

        //デバッグ用のパネルを生成
        _debugPanel = Instantiate(_debugPanelPrefab, this.transform);
        _debugPanel.SetActive(false);

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

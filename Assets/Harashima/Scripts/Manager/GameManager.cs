using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Range(1, 10), Tooltip("���X�e�[�W���邩")]
    int _stageLimit;
    public int StageLimit => _stageLimit;

    [Header("�f�o�b�O")]
    [SerializeField]
    bool _isDebug = false;
    [SerializeField]GameObject _debugPanelPrefab = default;
    GameObject _debugPanel = default;
    [SerializeField] GameObject _debugtextPrefab = default;
    GameObject _debugText = default;

    /// <summary>���݂̃X�e�[�W�N���A��</summary>
   [SerializeField]
    bool[] _clearedStage;
    public bool[] ClearedStage => _clearedStage;

    public int _friendShipPoints;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
        //�X�e�[�W�̐��ŏ�����
        _clearedStage = new bool[_stageLimit];
        _clearedStage[0] = true;
    }

    void Start()
    {
        //�f�o�b�O�p�̃p�l���𐶐�
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
    /// �X�e�[�W���N���A�����ۂ̏������s���֐�
    /// </summary>
    /// <param name="index">1�ȏ�̃X�e�[�W���ȉ��̒l</param>
    public void ClearStage(int index)
    {
        int num = index - 1;
        num = Mathf.Clamp(num, 0, _stageLimit - 1);
        _clearedStage[num] = true;
    }
    /// <summary>
    /// �F�D�|�C���g��Ԃ��֐�
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

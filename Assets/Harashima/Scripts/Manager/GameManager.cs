using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Range(1, 10), Tooltip("���X�e�[�W���邩")]
    int _stageLimit;

    [Header("�f�o�b�O")]
    [SerializeField]
    bool _isDebug = false;
    [SerializeField]
    GameObject _debugPanelPrefab = default;
    GameObject _debugPanel = default;

    /// <summary>���݂̃X�e�[�W�N���A��</summary>
    bool[] _clearedStage;

    public int _friendShipPoints;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        //�X�e�[�W�̐��ŏ�����
        _clearedStage = new bool[_stageLimit];

        //�f�o�b�O�p�̃p�l���𐶐�
        _debugPanel = Instantiate(_debugPanelPrefab, this.transform);
        _debugPanel.SetActive(false);

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

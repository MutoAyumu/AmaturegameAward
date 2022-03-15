using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField, Tooltip("Human�v���p�u������")] HumanController _human = default;
    [SerializeField, Tooltip("Ghost�v���p�u������")] GhostController _ghost = default;
    [SerializeField, Tooltip("�v�f0���l�ԁ@�v�f1���H��")] Transform[] _instancePos = new Transform[2];
    [SerializeField, Tooltip("Vcam������")] CinemachineVirtualCamera _vcam = default;
    [SerializeField] Text _lightCountTest = default;
    [SerializeField] Text _interactiveText = default;
    [SerializeField, Tooltip("����L������l�ԂɕύX����{�^���̖��O")] string _humanChangeButton = "RightTrigger";
    [SerializeField, Tooltip("����L������H��ɕύX����{�^���̖��O")] string _ghostChangeButton = "LeftTrigger";
    [SerializeField, Tooltip("��l�����Ă����{�^���̖��O")] string _togetherButton = "InputX";
    [SerializeField, Tooltip("����L������؂�ւ�����悤�ɂ���t���O")] bool _isCanSwitch;

    bool _isTogether;

    public HumanController Human { get => _human; }
    public GhostController Ghost { get => _ghost; }
    public CinemachineVirtualCamera Vcam { get => _vcam; set => _vcam = value; }
    public Text LightCountTest { get => _lightCountTest; }

    /*
        KeyCode��ς���
    */
    private void Start()
    {
        OnStart();
    }
    private void Update()
    {
        if (Input.GetButtonDown(_humanChangeButton) && _isCanSwitch)
        {
            HumanExchange();
            _interactiveText.gameObject.SetActive(false);
        }

        if(Input.GetButtonDown(_ghostChangeButton) && _isCanSwitch)
        {
            GhostExchange();
            _interactiveText.gameObject.SetActive(false);
        }

        if (_ghost.IsFixedRange && !_isTogether && _isCanSwitch)
        {
            MoveTogether();

            if(!_interactiveText.IsActive())
            {
                _interactiveText.gameObject.SetActive(true);
            }
        }
        else
        {
            if (_interactiveText.IsActive())
            {
                _interactiveText.gameObject.SetActive(false);
            }
        }

        if (_isTogether)
        {
            _ghost.transform.position = _human.GhostSetPos.position;
        }
    }
    /// <summary>
    /// �Q�[�����n�߂鏀��
    /// </summary>
    void OnStart()
    {
        _human = Instantiate(_human, _instancePos[0].position, Quaternion.identity);
        _ghost = Instantiate(_ghost, _instancePos[1].position, Quaternion.identity);

        _vcam.Follow = _human.transform;
        _human.IsControll = true;

        if(_interactiveText)
        {
            _interactiveText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("InteractiveText������܂���");
        }
    }
    /// <summary>
    /// ����L������l�Ԃɐ؂�ւ���֐�
    /// </summary>
    void HumanExchange()
    {
        _isTogether = false;
        _human.TogetherImage.gameObject.SetActive(false);
        _human.MainSprite.gameObject.SetActive(true);
        _ghost.MainSprite.gameObject.SetActive(true);
        _ghost.Stop();

        if (!_human.IsControll)
        {
            _ghost.IsControll = false;
            _human.IsControll = true;

            _vcam.Follow = _human.transform;
        }

    }
    /// <summary>
    /// ����L������H��ɐ؂�ւ���֐�
    /// </summary>
    void GhostExchange()
    {
        _isTogether = false;
        _human.TogetherImage.gameObject.SetActive(false);
        _human.MainSprite.gameObject.SetActive(true);
        _ghost.MainSprite.gameObject.SetActive(true);
        _human.Stop();

        if (!_ghost.IsControll)
        {
            _human.IsControll = false;
            _ghost.IsControll = true;

            _vcam.Follow = _ghost.transform;
        }
    }
    /// <summary>
    /// �ꏏ�ɓ������ɌĂ΂��
    /// </summary>
    void MoveTogether()
    {
        if (Input.GetButtonDown(_togetherButton) && !_isTogether)
        {
            _ghost.transform.DOMove(_human.GhostSetPos.position, 1)
                .OnStart(() =>
                {
                    _human.IsControll = false;
                    _human.Stop();
                })
                .OnComplete(() =>
                {
                    _isTogether = true;
                    _ghost.IsControll = false;
                    _human.IsControll = true;
                    _vcam.Follow = _human.transform;
                    _human.TogetherImage.gameObject.SetActive(true);
                    _human.MainSprite.gameObject.SetActive(false);
                    _ghost.MainSprite.gameObject.SetActive(false);
                });
        }
    }
    /// <summary>
    /// ���ݑ��삵�Ă���L������Ԃ�
    /// </summary>
    /// <returns></returns>
    public CharacterControllerBase CurrentOperation()
    {
        if(_human.IsControll)
        {
            return _human;
        }

        return _ghost;
    }
}

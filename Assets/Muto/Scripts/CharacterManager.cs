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

    [Header("UI")]
    [SerializeField] Text _lightCountTest = default;
    [SerializeField] Image _toPanel = default;
    [SerializeField] Sprite _humanImage = default;
    [SerializeField] Sprite _ghostImage = default;
    [SerializeField] Sprite _toImage = default;
    [SerializeField] Image _playerUiImage = default;

    [SerializeField] Transform _hpPanel = default;
    [SerializeField] Sprite _hpSprite = default;
    [SerializeField] Vector2 _hpSpriteSize = new Vector2(100f, 100f);

    [SerializeField] Transform _lightPanel = default;
    [SerializeField] Sprite _lightSprite = default;
    [SerializeField] Vector2 _lightSpriteSize = new Vector2(80f, 80f);

    [Header("�{�^���̐ݒ�")]
    [SerializeField, Tooltip("����L������l�ԂɕύX����{�^���̖��O")] string _humanChangeButton = "RightTrigger";
    [SerializeField, Tooltip("����L������H��ɕύX����{�^���̖��O")] string _ghostChangeButton = "LeftTrigger";
    [SerializeField, Tooltip("��l�����Ă����{�^���̖��O")] string _togetherButton = "InputX";
    [Header("������Q�[�W�֌W")]
    [SerializeField, Tooltip("������Q�[�W���Z�b�g")] Slider _warmthSlider = default;
    [SerializeField, Tooltip("���ꂷ�������ɕ\��������e�L�X�g")] Text _warmthText = default;
    [SerializeField, Tooltip("�L�����̗������Ԋu")] float _maxSpacing = 5f;
    [SerializeField] float _timeLimit = 3f;
    float _timer;
    [SerializeField]float _nakayoshiPoint;

    [SerializeField, Tooltip("����L������؂�ւ�����悤�ɂ���t���O")] bool _isCanSwitch = true;

    bool _isTogether;

    [Header("CutScene")]
    bool _cutSceneFlag;
    [SerializeField] GameObject _cutAvater = default;
    [SerializeField] Canvas _canvas = default;

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
        //�l�Ԃɐ؂�ւ���
        if (Input.GetButtonDown(_humanChangeButton) && _isCanSwitch)
        {
            HumanExchange();
            _toPanel.gameObject.SetActive(false);
        }

        //�H��ɐ؂�ւ���
        if(Input.GetButtonDown(_ghostChangeButton) && _isCanSwitch)
        {
            GhostExchange();
            _toPanel.gameObject.SetActive(false);
        }

        //�ꏏ�ɍs������
        if (_ghost.IsFixedRange && !_isTogether && _isCanSwitch)
        {
            MoveTogether();

            if(!_toPanel.IsActive())
            {
                _toPanel.gameObject.SetActive(true);
            }
        }
        else
        {
            if (_toPanel.IsActive())
            {
                _toPanel.gameObject.SetActive(false);
            }
        }

        //�ꏏ�ɍs�����Ă��鎞�͗H��̍��W���X�V����
        if (_isTogether)
        {
            _ghost.transform.position = _human.GhostSetPos.position;
            _ghost.Anim.SetFloat("X", _human.InputH);
            _ghost.Anim.SetFloat("Y", _human.InputV);

            _nakayoshiPoint += Time.deltaTime;
        }

        //������Q�[�W���X�V
        if(_warmthSlider)
        {
            _warmthSlider.value = 1 - CharacterSpacing() / _maxSpacing;

            //������Q�[�W��0�ȉ��ɂȂ�����e�L�X�g��\������
            if (_warmthSlider.value <= 0)
            {
                _warmthText.gameObject.SetActive(true);
                _timer += Time.deltaTime;

                if (_timeLimit <= _timer)
                {
                    _human.Hp.Damage();
                    _ghost.Hp.DamageAnim();
                    Debug.Log("�_���[�W���^����ꂽ");
                    _timer = 0;
                }

            }
            else if (_warmthText.IsActive())
            {
                _warmthText.gameObject.SetActive(false);
                _timer = 0;
            }
        } 
    }
    /// <summary>
    /// �Q�[�����n�߂鏀��
    /// </summary>
    void OnStart()
    {
        _human = Instantiate(_human, _instancePos[0].position, Quaternion.identity);
        _ghost = Instantiate(_ghost, _instancePos[1].position, Quaternion.identity);

        if (!_isCanSwitch)
        {
            _human.gameObject.SetActive(false);
            _canvas.gameObject.SetActive(false);

            _cutAvater.SetActive(true);
            _cutAvater.transform.position = _instancePos[0].position;
            _vcam.Follow = _cutAvater.transform;
        }
        else
        {
            if (_cutAvater)
            {
                Destroy(_cutAvater.gameObject);
            }

            HumanExchange();
        }

        if(_toPanel)
        {
            _toPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("InteractiveText������܂���");
        }

        if (!_warmthText)
        {
            Debug.Log("������e�L�X�g���Z�b�g����Ă��܂���");
        }
        else
        {
            _warmthText.gameObject.SetActive(false);
        }

        if (!_warmthSlider)
        {
            Debug.Log("������Q�[�W���Z�b�g����Ă��܂���");
        }
    }
    /// <summary>
    /// ����L������l�Ԃɐ؂�ւ���֐�
    /// </summary>
    void HumanExchange()
    {
        _human.TogetherImage.gameObject.SetActive(false);
        _human.MainSprite.gameObject.SetActive(true);
        _ghost.MainSprite.gameObject.SetActive(true);
        _ghost.Stop();

        if (!_human.IsControll)
        {
            _ghost.IsControll = false;
            _human.IsControll = true;
            _playerUiImage.sprite = _humanImage;

            _vcam.Follow = _human.transform;
        }

        if(_isTogether)
        {
            _human.Anim.Play("IdleTree");
            _isTogether = false;
            _playerUiImage.sprite = _humanImage;
        }

    }
    /// <summary>
    /// ����L������H��ɐ؂�ւ���֐�
    /// </summary>
    void GhostExchange()
    {
        _human.TogetherImage.gameObject.SetActive(false);
        _human.MainSprite.gameObject.SetActive(true);
        _ghost.MainSprite.gameObject.SetActive(true);
        _human.Stop();

        if (!_ghost.IsControll)
        {
            _human.IsControll = false;
            _ghost.IsControll = true;
            _playerUiImage.sprite = _ghostImage;

            _vcam.Follow = _ghost.transform;
        }

        if (_isTogether)
        {
            _human.Anim.Play("IdleTree");
            _isTogether = false;
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
                    _ghost.Col.isTrigger = true;
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
                    _human.Anim.Play("ToIdleTree");
                    _ghost.Col.isTrigger = false;
                    _playerUiImage.sprite = _toImage;
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

    /// <summary>
    /// �L�����̊Ԋu��Ԃ�
    /// </summary>
    /// <returns></returns>
    float CharacterSpacing()
    {
        if (_human && _ghost)
        {
            return Vector2.Distance(Human.transform.position, Ghost.transform.position);
        }

        return 0;
    }

    /// <summary>
    /// ����L�����̐؂�ւ����\�ɂ���
    /// </summary>
    public void Switching()
    {
        _isCanSwitch = true;
    }

    /// <summary>
    /// ����q���ł��鎞�Ԃ�Ԃ��֐�
    /// </summary>
    /// <returns></returns>
    public float ReturnPoint()
    {
        return _nakayoshiPoint;
    }
    public void UIHPUpdate(int num)
    {
        //��U����
        foreach(Transform t in _hpPanel.transform)
        {
            Destroy(t.gameObject);
        }

        //UI�̍X�V
        for(int i = 0; i < num; i++)
        {
            var go = new GameObject();
            var image = go.AddComponent<Image>();

            image.sprite = _hpSprite;
            var r = go.GetComponent<RectTransform>();
            r.sizeDelta = _hpSpriteSize;

            go.transform.SetParent(_hpPanel.transform);
        }
    }
    public void UILightUpdate(int num)
    {
        foreach(Transform t in _lightPanel.transform)
        {
            Destroy(t.gameObject);
        }

        for(int i = 0; i < num; i++)
        {
            var go = new GameObject();
            var image = go.AddComponent<Image>();

            image.sprite = _lightSprite;
            var r = go.GetComponent<RectTransform>();
            r.sizeDelta = _lightSpriteSize;

            go.transform.SetParent(_lightPanel.transform);
        }
    }
    public void StartCutScene()
    {

        _human.gameObject.SetActive(true);
        _canvas.gameObject.SetActive(true); ;
        _human.transform.position = _cutAvater.transform.position;

        HumanExchange();

        Destroy(_cutAvater.gameObject);
    }
}

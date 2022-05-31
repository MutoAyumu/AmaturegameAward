using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

/// <summary>
/// �t�B�[���h��ł̐i�s���Ǘ�����N���X�i�f�o�b�O���_�ł�UIManager������Ă���j
/// </summary>
public class FieldManager : Singleton<FieldManager>
{
    /// <summary>�X�^�[�g���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnStart;

    /// <summary>���U���g���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnClear;

    /// <summary>�Q�[���I�[�o�[���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnGameOver;

    /// <summary>�|�[�Y���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnPause;

    /// <summary>�ĊJ���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnResume;

    public event Action OnTextPause;
    public event Action OnTextResume;

    /// <summary>�N���A���Q�[���I�[�o�[�𔻒肷��t���O</summary>
    bool _isDead = false;

    [SerializeField, Range(1, 10), Tooltip("�X�e�[�W�̔ԍ�")]
    int _stageIndex;
    public int StageIndex => _stageIndex;

    [SerializeField, Tooltip("�t�B�[���hBGM")]
    AudioClip _bgm;

    [SerializeField, Tooltip("�V�[����̃L�����o�X")]
    GameObject _canvas;

    [Header("�M�~�b�N�쓮���Ɏg���镨")]
    [SerializeField, Tooltip("�M�~�b�N���쓮�������Ɏg���J����")] CinemachineVirtualCamera _eventCam = default;
    [SerializeField] GameObject _eventPanel = default;
    [SerializeField] GameObject _playerCanvas = default;

    [SerializeField] string _pauseInputName = "Cancel";
    bool IsPause;
    [SerializeField] Image _pausePanel = default;
    /// <summary>�V�[����L�����o�X�̓ǂݎ��v���p�e�B</summary>
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
        //�e�X�g�p
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

        if (PlayerPalam.Instance?.Life <= 0 && !_isDead)�@//�X�R�A��0�ɂȂ�AisEnd��False��������
        {
            if(OnGameOver != null)
            {
                //�Q�[���I�[�o�[�C�x���g���Ă�
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
        //�N���A�C�x���g���Ă�
        if(OnClear!= null && !_isDead)//isEnd��False��������
        {
            OnClear();
            _isDead = true;
        }

        //�N���A���̏������Ă�
        GameManager.Instance?.ClearStage(_stageIndex);
    }

    [SerializeField, Tooltip("�f�o�b�O�p�̃��U���g�p�l��")]
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
        Debug.Log("�Q�[���I�[�o�[");
    }

    void DebugClear()
    {
        //_resultPanel.SetActive(true);
        //_timeLine.Play();
        Debug.Log("�N���A");
    }

    //�ȉ��d�l�̎���
    [SerializeField,Tooltip("�A�C�e����\������p�l��")]
    GameObject[] _inventryPanels;
    public GameObject[] InventryPanels => _inventryPanels;

    [SerializeField, Tooltip("�A�C�e����\������C���[�W")]
    GameObject[] _inventryImage;

    [SerializeField,Tooltip("�A�C�e���̌���\������UIText")]
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
    /// �f�o�b�O�p�B�C���v�b�g���󂯎��֐��i���j
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


    //�ȉ��̓��ł̃A�C�e���̎d�l
    [SerializeField]
    GameObject _inventryButton;

    /// <summary>
    /// ���̊֐�
    /// </summary>
    public void ChoiceActive(bool active)
    {
        _inventryButton.SetActive(active);
    }

    /// <summary>
    /// �f�o�b�O�{�^���p�A�w�肵��Index�̃C���x���g�����폜����
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
            Debug.LogError("EventCamera��EventPanel���Z�b�g����Ă��܂���");
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

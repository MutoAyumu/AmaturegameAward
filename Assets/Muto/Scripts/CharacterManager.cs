using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField, Tooltip("Humanプレパブを入れる")] HumanController _human = default;
    [SerializeField, Tooltip("Ghostプレパブを入れる")] GhostController _ghost = default;
    [SerializeField, Tooltip("要素0が人間　要素1が幽霊")] Transform[] _instancePos = new Transform[2];
    [SerializeField, Tooltip("Vcamを入れる")] CinemachineVirtualCamera _vcam = default;
    [SerializeField] CinemachineImpulseSource _source = default;
    [SerializeField] CinemachineImpulseSource _toSource = default;

    [Header("UI")]
    [SerializeField] Text _lightCountTest = default;
    [SerializeField] Image _toPanel = default;
    [SerializeField] Sprite _humanImage = default;
    [SerializeField] Sprite _ghostImage = default;
    [SerializeField] Sprite _toImage = default;
    [SerializeField] Image _playerUiImage = default;

    [SerializeField] Transform _hpPanel = default;
    [SerializeField, Tooltip("ハートの左半分")] Sprite _hpSpriteR = default;
    [SerializeField, Tooltip("ハートの右半分")] Sprite _hpSpriteL = default;
    [SerializeField] Vector2 _hpSpriteSize = new Vector2(100f, 100f);

    [SerializeField] Transform _lightPanel = default;
    [SerializeField] Sprite _lightSprite = default;
    [SerializeField] Vector2 _lightSpriteSize = new Vector2(80f, 80f);

    [Header("ボタンの設定")]
    [SerializeField, Tooltip("操作キャラを人間に変更するボタンの名前")] string _humanChangeButton = "RightTrigger";
    [SerializeField, Tooltip("操作キャラを幽霊に変更するボタンの名前")] string _ghostChangeButton = "LeftTrigger";
    [SerializeField, Tooltip("二人がついていくボタンの名前")] string _togetherButton = "InputX";
    [Header("温もりゲージ関係")]
    [SerializeField, Tooltip("温もりゲージをセット")] Slider _warmthSlider = default;
    [SerializeField, Tooltip("離れすぎた時に表示させるテキスト")] Text _warmthText = default;
    [SerializeField, Tooltip("キャラの離れられる間隔")] float _maxSpacing = 5f;
    [SerializeField] float _fixedRange = 1f;
    [SerializeField] float _timeLimit = 3f;
    float _timer;
    [SerializeField] float _nakayoshiPoint;

    [SerializeField] Text _intaractText = default;
    [SerializeField] Animator _connectImage = default;

    [SerializeField, Range(0,1)] float _ghostAlpha = default;

    [SerializeField, Tooltip("操作キャラを切り替えられるようにするフラグ")] bool _isCanSwitch = true;
    [SerializeField, Tooltip("幽霊が攻撃できるようになるフラグ")] bool _isGhostAttack;

    [SerializeField] bool _isGodMode;

    [Space(10), Header("人間・幽霊のセリフ")]
    [SerializeField] string[] _humanMessage;
    [SerializeField] string[] _ghostMessage;

    bool _isTogether;
    bool _isFixedRange;

    FieldManager _fieldManager;

    public HumanController Human { get => _human; }
    public GhostController Ghost { get => _ghost; }
    public CinemachineVirtualCamera Vcam { get => _vcam; set => _vcam = value; }
    public Text LightCountTest { get => _lightCountTest; }
    public bool IsTogether { get => _isTogether; }
    public bool IsGhostAttack { get => _isGhostAttack; }
    public string[] HumanMessage { get => _humanMessage; }
    public string[] GhostMessage { get => _ghostMessage; }
    public bool GodMode { get => _isGodMode;}
    public float GhostAlpha { get => _ghostAlpha;}

    /*
        KeyCodeを変える
    */
    private void Start()
    {
        OnStart();

        _fieldManager = FieldManager.Instance;
        _fieldManager.OnStart += HumanExchange;
        _fieldManager.OnPause += _human.Pause;
        _fieldManager.OnPause += _ghost.Pause;
        _fieldManager.OnResume += _human.Resume;
        _fieldManager.OnResume += _ghost.Resume;
        _fieldManager.OnTextPause += _human.PauseRb;
        _fieldManager.OnTextPause += _ghost.PauseRb;
        _fieldManager.OnTextResume += _human.ResumeRb;
        _fieldManager.OnTextResume += _ghost.ResumeRb;
    }
    private void Update()
    {
        if (TimeLineManager.Instance)
        {
            if (TimeLineManager.Instance.CutSceneFlag)
                return;
        }

        if (_fieldManager.IsDead) return;

        //人間に切り替える
        if (Input.GetButtonDown(_humanChangeButton) && _isCanSwitch)
        {
            HumanExchange();
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SwitchPlayer");
            _toPanel.gameObject.SetActive(false);
        }

        //幽霊に切り替える
        if (Input.GetButtonDown(_ghostChangeButton) && _isCanSwitch)
        {
            GhostExchange();
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SwitchPlayer");
            _toPanel.gameObject.SetActive(false);
        }

        //一緒に行動する
        if (_isFixedRange && !_isTogether && _isCanSwitch)
        {
            if (Input.GetButtonDown(_togetherButton) && !_isTogether)
            {
                MoveTogether();
                _isFixedRange = false;
                _toSource.GenerateImpulse();
            }

            if (!_toPanel.IsActive())
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

        //一緒に行動している時は幽霊の座標を更新する
        if (_isTogether)
        {
            _ghost.transform.position = _human.GhostSetPos.position;
            _ghost.Anim.SetFloat("X", _human.InputH);
            _ghost.Anim.SetFloat("Y", _human.InputV);

            _nakayoshiPoint += Time.deltaTime;

            if (_nakayoshiPoint >= 30)
            {
                GameManager.Instance._friendShipPoints++;
                _nakayoshiPoint = 0;
            }
        }

        //温もりゲージを更新
        if (_warmthSlider)
        {
            if (_warmthSlider.IsActive())
            {
                if (!_isTogether)
                    _warmthSlider.value = 1 - CharacterSpacing() / _maxSpacing;
                else
                    _warmthSlider.value = 1;



                //温もりゲージが0以下になったらテキストを表示する
                if (_warmthSlider.value <= 0)
                {
                    _warmthText.gameObject.SetActive(true);
                    _timer += Time.deltaTime;

                    if (_timeLimit <= _timer)
                    {
                        _human.Hp.Damage();
                        _ghost.IsDamageAction();
                        Debug.Log("ダメージが与えられた");
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
        if(!IsTogether)
        {
            if(_isFixedRange && _connectImage.GetBool("Set"))
            {
                SetConnectImage(false);
            }
            else if(!_isFixedRange && !_connectImage.GetBool("Set"))
            {
                SetConnectImage(true);
            }
        }
        else
        {
            if(!_connectImage.GetBool("Set"))
            {
                SetConnectImage(true);
            }
        }

        var dir = Vector2.Distance(_human.ColliderCenter(), _ghost.ColliderCenter());

        if(dir <= _fixedRange && !_isFixedRange)
        {
            _isFixedRange = true;
        }
        else if(dir > _fixedRange && _isFixedRange)
        {
            _isFixedRange = false;
        }
    }
    /// <summary>
    /// ゲームを始める準備
    /// </summary>
    void OnStart()
    {
        _human = Instantiate(_human, _instancePos[0].position, Quaternion.identity);
        _ghost = Instantiate(_ghost, _instancePos[1].position, Quaternion.identity);

        HumanExchange();

        if (_toPanel)
        {
            _toPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("InteractiveTextがありません");
        }

        if (!_warmthText)
        {
            Debug.Log("温もりテキストがセットされていません");
        }
        else
        {
            _warmthText.gameObject.SetActive(false);
        }

        if (!_warmthSlider)
        {
            Debug.Log("温もりゲージがセットされていません");
        }
    }
    /// <summary>
    /// 操作キャラを人間に切り替える関数
    /// </summary>
    public void HumanExchange()
    {
        _human.TogetherImage.gameObject.SetActive(false);
        _human.MainSprite.gameObject.SetActive(true);
        _ghost.gameObject.SetActive(true);
        _ghost.Stop();
        _ghost.ChangerMessageFlag(true);

        if (!_human.IsControll)
        {
            _ghost.IsControll = false;
            _human.IsControll = true;
            _playerUiImage.sprite = _humanImage;

            _vcam.Follow = _human.transform;
        }

        if (_isTogether)
        {
            _isTogether = false;
            _human.ChangerMessageFlag(false);
            _human.Anim.SetBool("IsTogether", _isTogether);
            _playerUiImage.sprite = _humanImage;
        }
    }
    /// <summary>
    /// 操作キャラを幽霊に切り替える関数
    /// </summary>
    void GhostExchange()
    {
        _human.TogetherImage.gameObject.SetActive(false);
        _human.MainSprite.gameObject.SetActive(true);
        _ghost.gameObject.SetActive(true);
        _human.Stop();
        _human.ChangerMessageFlag(true);

        if (!_ghost.IsControll)
        {
            _human.IsControll = false;
            _ghost.IsControll = true;
            _playerUiImage.sprite = _ghostImage;

            _vcam.Follow = _ghost.transform;
        }

        if (_isTogether)
        {
            _isTogether = false;
            _ghost.MainSprite.DOFade(_ghost.Alpha, 1);
            _human.Anim.SetBool("IsTogether", _isTogether);
        }
    }
    /// <summary>
    /// 一緒に動く時に呼ばれる
    /// </summary>
    public void MoveTogether()
    {
        _ghost.transform.DOMove(_human.GhostSetPos.position, 1)
            .OnStart(() =>
            {
                _human.IsControll = false;
                _human.Stop();
                //_ghost.Col.isTrigger = true;
                _ghost.ChangerMessageFlag(false);
                _ghost.MainSprite.DOFade(1f, 1);
            })
            .OnComplete(() =>
            {
                SoundManager.Instance.CriAtomPlay(CueSheet.SE, "Hand");
                _isTogether = true;
                _ghost.IsControll = false;
                _human.IsControll = true;
                _vcam.Follow = _human.transform;
                _human.TogetherImage.gameObject.SetActive(true);
                _human.MainSprite.gameObject.SetActive(false);
                _ghost.gameObject.SetActive(false);
                _human.Anim.SetBool("IsTogether", _isTogether);
                //_ghost.Col.isTrigger = false;
                _playerUiImage.sprite = _toImage;
            });
    }
    /// <summary>
    /// 現在操作しているキャラを返す
    /// </summary>
    /// <returns></returns>
    public CharacterControllerBase CurrentOperation()
    {
        if (_human.IsControll)
        {
            return _human;
        }

        return _ghost;
    }

    /// <summary>
    /// キャラの間隔を返す
    /// </summary>
    /// <returns></returns>
    float CharacterSpacing()
    {
        if (_human && _ghost)
        {
            return Vector2.Distance(Human.ColliderCenter(), Ghost.ColliderCenter());
        }

        return 0;
    }

    /// <summary>
    /// 操作キャラの切り替えを可能にする
    /// </summary>
    public void Switching()
    {
        _isCanSwitch = true;
    }
    public void SwitchingAttackFlag()
    {
        _isGhostAttack = true;
    }
    public void UIHPUpdate(int num)
    {
        //一旦消す
        foreach (Transform t in _hpPanel.transform)
        {
            Destroy(t.gameObject);
        }

        //UIの更新
        for (int i = 1; i < num + 1; i++)
        {
            var go = new GameObject();
            var image = go.AddComponent<Image>();

            if (i % 2 != 0)
                image.sprite = _hpSpriteL;
            else
                image.sprite = _hpSpriteR;

            var r = go.GetComponent<RectTransform>();
            r.sizeDelta = _hpSpriteSize;

            go.transform.SetParent(_hpPanel.transform);
        }
    }
    public void UILightUpdate(int num)
    {
        foreach (Transform t in _lightPanel.transform)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < num; i++)
        {
            var go = new GameObject();
            var image = go.AddComponent<Image>();

            image.sprite = _lightSprite;
            var r = go.GetComponent<RectTransform>();
            r.sizeDelta = _lightSpriteSize;

            go.transform.SetParent(_lightPanel.transform);
        }
    }
    public void CharacterDead()
    {
        _human.IsDead();
        _ghost.IsDead();
        var s = SoundManager.Instance;
        s.CriAtomStop();
        s.CriAtomPlay(CueSheet.ME, "MEDeath");
        s.CriAtomPlay(CueSheet.SE, "HumanDeath");
        s.CriAtomPlay(CueSheet.SE, "GhostDeath");
    }


    /// <summary>
    /// 近いほうのキャラクターを返す関数
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>比較先のTransform</returns>
    public Transform PlayerPosition(Transform pos)
    {
        Vector3 player1 = _human.ColliderCenter();
        Debug.DrawLine(pos.position, player1);
        float isHit1 = Vector3.Distance(pos.position, player1);

        Vector3 player2 = _ghost.ColliderCenter();
        Debug.DrawLine(pos.position, player2);
        float isHit2 = Vector3.Distance(pos.position, player2);

        if (isHit1 < isHit2)
        {
            Debug.DrawLine(pos.position, player1, Color.red);
            return _human.transform;
        }
        else
        {
            Debug.DrawLine(pos.position, player2, Color.red);
            return _ghost.transform;
        }
    }
    public void SetIntaractText(string text)
    {
        if (!_intaractText) return;

        _intaractText.text = text;
    }
    public void ClearIntaractText()
    {
        _intaractText.text = "";
    }
    public void SetConnectImage(bool value)
    {
        if (!_connectImage) return;

        _connectImage.SetBool("Set" ,value);
    }

    public void SetResetPos(Vector3 humanPos,Vector3 ghostPos)
    {
        _human.ResetPos = humanPos;
        _ghost.ResetPos = ghostPos;
    }
    public void SetResetPos()
    {
        _human.ResetPos = _human.transform.position;
        _ghost.ResetPos = _ghost.transform.position;
    }

}

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
    [SerializeField] Text _lightCountTest = default;
    [SerializeField] Text _interactiveText = default;
    [SerializeField, Tooltip("操作キャラを人間に変更するボタンの名前")] string _humanChangeButton = "RightTrigger";
    [SerializeField, Tooltip("操作キャラを幽霊に変更するボタンの名前")] string _ghostChangeButton = "LeftTrigger";
    [SerializeField, Tooltip("二人がついていくボタンの名前")] string _togetherButton = "InputX";
    [SerializeField] Slider _warmthSlider = default;
    [SerializeField] Text _warmthText = default;
    [SerializeField] float _maxSpacing = 5f;
    [SerializeField, Tooltip("操作キャラを切り替えられるようにするフラグ")] bool _isCanSwitch;

    bool _isTogether;

    public HumanController Human { get => _human; }
    public GhostController Ghost { get => _ghost; }
    public CinemachineVirtualCamera Vcam { get => _vcam; set => _vcam = value; }
    public Text LightCountTest { get => _lightCountTest; }

    /*
        KeyCodeを変える
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

        if(_warmthSlider)
        {
            _warmthSlider.value = 1 - CharacterSpacing() / _maxSpacing;
        }
        if(_warmthSlider.value <= 0)
        {
            _warmthText.gameObject.SetActive(true);
        }
        else if(_warmthText.gameObject.activeSelf)
        {
            _warmthText.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// ゲームを始める準備
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
            Debug.LogError("InteractiveTextがありません");
        }

        if (!_warmthText)
        {
            Debug.LogError("温もりテキストがセットされていません");
        }
        else
        {
            _warmthText.gameObject.SetActive(false);
        }

        if (!_warmthSlider)
        {
            Debug.LogError("温もりゲージがセットされていません");
        }
    }
    /// <summary>
    /// 操作キャラを人間に切り替える関数
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
    /// 操作キャラを幽霊に切り替える関数
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
    /// 一緒に動く時に呼ばれる
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
    /// 現在操作しているキャラを返す
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
    /// キャラの間隔を返す
    /// </summary>
    /// <returns></returns>
    float CharacterSpacing()
    {
        return Vector2.Distance(Human.transform.position, Ghost.transform.position);
    }

}

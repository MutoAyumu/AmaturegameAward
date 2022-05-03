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

    [Header("ボタンの設定")]
    [SerializeField, Tooltip("操作キャラを人間に変更するボタンの名前")] string _humanChangeButton = "RightTrigger";
    [SerializeField, Tooltip("操作キャラを幽霊に変更するボタンの名前")] string _ghostChangeButton = "LeftTrigger";
    [SerializeField, Tooltip("二人がついていくボタンの名前")] string _togetherButton = "InputX";
    [Header("温もりゲージ関係")]
    [SerializeField, Tooltip("温もりゲージをセット")] Slider _warmthSlider = default;
    [SerializeField, Tooltip("離れすぎた時に表示させるテキスト")] Text _warmthText = default;
    [SerializeField, Tooltip("キャラの離れられる間隔")] float _maxSpacing = 5f;
    [SerializeField] float _timeLimit = 3f;
    float _timer;
    [SerializeField] float _nakayoshiPoint;

    [SerializeField, Tooltip("操作キャラを切り替えられるようにするフラグ")] bool _isCanSwitch = true;

    bool _isTogether;

    public HumanController Human { get => _human; }
    public GhostController Ghost { get => _ghost; }
    public CinemachineVirtualCamera Vcam { get => _vcam; set => _vcam = value; }
    public Text LightCountTest { get => _lightCountTest; }
    public bool IsTogether { get => _isTogether; }

    /*
        KeyCodeを変える
    */
    private void Start()
    {
        OnStart();
    }
    private void Update()
    {
        //人間に切り替える
        if (Input.GetButtonDown(_humanChangeButton) && _isCanSwitch)
        {
            HumanExchange();
            _toPanel.gameObject.SetActive(false);
        }

        //幽霊に切り替える
        if (Input.GetButtonDown(_ghostChangeButton) && _isCanSwitch)
        {
            GhostExchange();
            _toPanel.gameObject.SetActive(false);
        }

        //一緒に行動する
        if (_ghost.IsFixedRange && !_isTogether && _isCanSwitch)
        {
            if (Input.GetButtonDown(_togetherButton) && !_isTogether)
            {
                MoveTogether();
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

            if(_nakayoshiPoint >= 30)
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
                _warmthSlider.value = 1 - CharacterSpacing() / _maxSpacing;

                //温もりゲージが0以下になったらテキストを表示する
                if (_warmthSlider.value <= 0)
                {
                    _warmthText.gameObject.SetActive(true);
                    _timer += Time.deltaTime;

                    if (_timeLimit <= _timer)
                    {
                        _human.Hp.Damage();
                        _ghost.Hp.DamageAnim();
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

        if (_isTogether)
        {
            _isTogether = false;
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
            _isTogether = false;
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
                _human.Anim.SetBool("IsTogether", _isTogether);
                _ghost.Col.isTrigger = false;
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
            return Vector2.Distance(Human.transform.position, Ghost.transform.position);
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
    public void UIHPUpdate(int num)
    {
        //一旦消す
        foreach (Transform t in _hpPanel.transform)
        {
            Destroy(t.gameObject);
        }

        //UIの更新
        for (int i = 0; i < num; i++)
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
}

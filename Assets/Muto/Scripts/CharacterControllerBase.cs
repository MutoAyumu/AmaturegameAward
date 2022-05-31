using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb = default;
    [SerializeField] protected Collider2D _col = default;
    [SerializeField] protected PlayerHP _hp = default;
    [SerializeField] string _endAreaTag = "Finish";
    [SerializeField] protected Animator _anim = default;
    [SerializeField] protected SpriteRenderer _mainSprite = default;
    [SerializeField] ObjectSearcher _searchar = default;
    [SerializeField, Tooltip("Searcharを呼ぶときのボタンの名前")] string _inputSearchar = "Fire2";
    [SerializeField, Tooltip("Rayの長さ")] protected float _rayLength = 1f;
    [Header("操作キャラのパラメーター"), Space(10)]
    [SerializeField] protected float _moveSpeed = 3.0f;
    [SerializeField] protected CharacterStatus _status = CharacterStatus.IDLE;
    [SerializeField] protected GameObject _deathPrefab = null;

    Vector3 _resetPos = Vector3.zero;
    public Vector3 ResetPos { get => _resetPos; set => _resetPos = value; }

    [Tooltip("最後に入力された横方向の値")] protected float _lh = 1;
    public float InputH => _lh;
    [Tooltip("最後に入力された縦方向の値")] protected float _lv = default;
    public float InputV => _lv;

    [Tooltip("どっちのキャラが操作されているかのフラグ")] protected bool _isControll = default;

    [SerializeField, Tooltip("ステージクリアで使うトリガーのタグ")] string _endTag = "Finish";

    protected float _h = default;
    protected float _v = default;
    [SerializeField] protected float _currentSpeed;

    [SerializeField] protected MessageCharacter _message = default;

    [SerializeField] protected CriAtomSource _atomSource = default;

    FieldManager _fieldManager;

    bool IsPause;
    bool IsTextPause;
    protected bool IsSetText;

    RaycastHit2D _hit;
    protected Coroutine _coroutine;
    protected bool _isDamage;

    public bool IsControll { get => _isControll; set => _isControll = value; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public PlayerHP Hp { get => _hp; }
    public SpriteRenderer MainSprite { get => _mainSprite; }
    public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }
    public Animator Anim { get => _anim; }
    public Collider2D Col { get => _col; }
    public bool IsDamage { get => _isDamage;}


    protected enum CharacterStatus
    {
        IDLE,
        WALK,
        ATTACK,
        DAMAGE,
        ACTION,
        Dead
    }

    private void OnEnable()
    {
        //_fieldManager.OnPause += Pause;
        //_fieldManager.OnResume += Resume;

        //_fieldManager.OnTextPause += PauseRb;
        //_fieldManager.OnTextResume += ResumeRb;

    }
    private void OnDisable()
    {
        //_fieldManager.OnPause -= Pause;
        //_fieldManager.OnResume -= Resume;

        //_fieldManager.OnTextPause -= PauseRb;
        //_fieldManager.OnTextResume -= ResumeRb;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _isDamage = false;
        Stop();
    }
    private void Awake()
    {
        _fieldManager = FieldManager.Instance;
        OnAwake();
    }
    public virtual void OnAwake()
    {

    }
    private void Start()
    {
        _currentSpeed = _moveSpeed;
        _resetPos = this.transform.position;
        FieldManager.Instance.OnStart += InitPos;
        OnStart();
    }

    void InitPos()
    {
        this.gameObject.SetActive(true);
        this.transform.position = _resetPos;
        _status = CharacterStatus.WALK;
        //_anim.SetBool("IsDead", false);
        if(_currentDead)
        {
            Destroy(_currentDead);
        }
    }

    public void DestroyDeathPrefab()
    {
        if (_currentDead)
        {
            Destroy(_currentDead);
        }
    }
    public virtual void OnStart()
    {

    }
    private void Update()
    {
        if (_status == CharacterStatus.Dead)
        {
            return;
        }

        DamageAnim();

        if (TimeLineManager.Instance)
        {
            if (TimeLineManager.Instance.CutSceneFlag)
                return;
        }

        if (_isControll && !IsPause && !IsTextPause)
        {
            InputValue();

            //状態を管理する
            switch (_status)
            {
                case CharacterStatus.IDLE:
                    _anim.SetBool("IsMove", false);

                    if (Input.GetButtonDown(_inputSearchar) && _searchar)
                    {
                        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(_lh, _lv), _rayLength, _searchar.Layer);
                        _searchar.Search(_lh, _lv, _hit);
                    }
                    break;

                case CharacterStatus.WALK:
                    Move(_h, _v);
                    _anim.SetBool("IsMove", true);

                    if (Input.GetButtonDown(_inputSearchar) && _searchar)
                    {
                        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(_lh, _lv), _rayLength, _searchar.Layer);
                        _searchar.Search(_lh, _lv, _hit);
                    }
                    break;

                case CharacterStatus.ATTACK:
                    _rb.velocity = Vector2.zero;
                    break;

                case CharacterStatus.DAMAGE:
                    break;

                case CharacterStatus.ACTION:
                    Move(_h, _v);
                    _anim.SetBool("IsMove", true);
                    break;

                case CharacterStatus.Dead:
                    break;

                default:
                    break;
            }

            OnUpdate();
        }
        else if (!IsControll)// && _interactImage.activeSelf)
        {
            //_interactImage.SetActive(false);
        }
    }
    /// <summary>
    /// 派生先で使うUpdate
    /// </summary>
    public virtual void OnUpdate()
    {

    }
    public virtual void DamageAnim()
    {

    }
    protected virtual void Interact()
    {
        _hit = Physics2D.Raycast(this.transform.position, new Vector2(_lh, _lv), _rayLength, _searchar.Layer);

        if (_hit && !IsSetText)
        {
            IsSetText = true;
            CharacterManager.Instance.SetIntaractText(_hit.collider?.GetComponent<ISetText>()?.SetText());
        }
        else if (!_hit && IsSetText)
        {
            IsSetText = false;
            CharacterManager.Instance.ClearIntaractText();
        }
    }
    /// <summary>
    /// 移動入力を受ける関数
    /// </summary>
    protected void InputValue()
    {
        if (_status == CharacterStatus.DAMAGE) return;

        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        if (_status != CharacterStatus.ATTACK && _status != CharacterStatus.ACTION)
        {
            if (_h == 0 && _v == 0)
            {
                _status = CharacterStatus.IDLE;
            }
            else
            {
                _status = CharacterStatus.WALK;
            }
        }

        if (_status != CharacterStatus.ACTION)
        {
            if (_h != 0 || _v != 0)
            {
                if (_lh != _h || _lv != _v)
                {
                    _h = RoundingUpDown(_h);
                    _v = RoundingUpDown(_v);
                    _lh = _h;
                    _lv = _v;
                }

                Interact();
            }
        }

        if (_anim)
        {
            _anim.SetFloat("X", _lh);
            _anim.SetFloat("Y", _lv);
        }
    }
    float RoundingUpDown(float f)
    {
        if(f > 0)
        {
            return Mathf.Ceil(f);
        }
        else
        {
            return Mathf.Floor(f);
        }
    }
    /// <summary>
    /// 操作キャラを動かす関数
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    protected void Move(float h, float v)
    {
        var dir = new Vector2(h, v).normalized;
        _rb.velocity = dir * _currentSpeed;

        Debug.DrawRay(this.transform.position, new Vector2(_lh, _lv).normalized * _rayLength, Color.red);
    }
    /// <summary>
    /// 操作キャラを止める関数
    /// </summary>
    public virtual void Stop()
    {
        if (_rb)
        {
            _rb.velocity = Vector2.zero;
            _anim.SetBool("IsMove", false);
        }
        else
        {
            Debug.LogError("Rigidbody2Dがありません");
        }
    }
    /// <summary>
    /// 最後に向いている方向を返す関数
    /// </summary>
    /// <returns></returns>
    public Vector2 ReturnTheDirection()
    {
        return new Vector2(_lh, _lv);
    }

    public Vector2 ColliderCenter()
    {
        return (Vector2)this.transform.position + _col.offset;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(_endTag))
    //    {
    //        FieldManager.Instance.Clear();
    //    }
    //}
    public void Pause()
    {
        _rb.Sleep();
        _anim.speed = 0;
        IsPause = true;
    }

    public void PauseRb()
    {
        _rb.Sleep();
        _anim.SetBool("IsMove", false);
        IsTextPause = true;
    }
    public void Resume()
    {
        _rb.WakeUp();
        _anim.speed = 1;
        IsPause = false;
    }
    public void ResumeRb()
    {
        _rb.WakeUp();
        IsTextPause = false;
    }

    public void ChangerMessageFlag(bool flag)
    {
        if (_message)
        {
            _message.IsMessage(flag);
        }
    }

    GameObject _currentDead = null;
    public void IsDead()
    {
        //_status = CharacterStatus.Dead;
        //_anim.SetBool("IsDead", true);

        this.gameObject.SetActive(false);
        if(_deathPrefab)
        {
            _currentDead = Instantiate(_deathPrefab,this.transform.position,Quaternion.identity);
        }
    }
    public virtual void IsDamageAction()
    {
        _rb.AddForce(-new Vector2(_lh, _lv).normalized * 20, ForceMode2D.Impulse);
        //StartCoroutine(DamageStart());
        //_status = CharacterStatus.DAMAGE;
    }
    protected virtual IEnumerator OnDamage(float alpha)
    {
        yield return new WaitForSeconds(2.0f);

        _isDamage = false;
        _mainSprite.color = new Color(1, 1, 1, alpha);
        _coroutine = null;
    }
    IEnumerator DamageStart()
    {
        yield return new WaitForSeconds(1.5f);
        _status = CharacterStatus.IDLE;
    }
}

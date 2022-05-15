using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;

public class LightSource : MonoBehaviour, ISetText
{
    [SerializeField] Light2D _light = default;
    [SerializeField] Animator _anim = default;
    [SerializeField] float _time = 0.5f;
    [SerializeField] float _loopTime = 10f;
    [SerializeField] bool _isOn = true;
    [SerializeField] bool _isLoop;
    [SerializeField] string _text = "B　光を取る";
    [SerializeField, Tooltip("IActivateを継承したGameObjectを入れる")] GameObject[] _activate = default;

    float _timer = default;

    public bool IsOn { get => _isOn; set => _isOn = value; }
    public Light2D Light { get => _light; set => _light = value; }

    /*ToDo
        
    */
    private void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        if (_isOn)
        {
            _light.intensity = 1;
            _anim.SetBool("Light", _isOn);
        }
        else
        {
            _light.intensity = 0;
            _anim.SetBool("Light", _isOn);
        }
    }
    private void Update()
    {
        if(_isLoop)
        {
            if(!_isOn)
            {
                _timer += Time.deltaTime;

                if(_timer >= _loopTime)
                {
                    _timer = 0;
                    Action(_time);
                    _isOn = true;
                }
            }
            else
            {
                if(_timer != 0)
                {
                    _timer = 0;
                }
            }
        }
    }
    /// <summary>
    /// ライトのオンオフ切り替えをする関数
    /// </summary>
    public Tween Action(float time)
    {
        if (_light)
        {
            if (_isOn)
            {
                //_isOn = false;
                _anim.SetBool("Light", false);
                return DOVirtual.Float(_light.intensity, 0, time, value => _light.intensity = value);
            }
            else if (!_isOn)
            {
                //_isOn = true;
                _anim.SetBool("Light", true);
                return DOVirtual.Float(_light.intensity, 1.0f, time, value => _light.intensity = value)
                    .OnComplete(() =>
                    {
                        foreach(var ac in _activate)
                        {
                            ac.GetComponent<IActivate>()?.Action();
                        }
                    });
            }
        }

        return null;
    }
    public string SetText()
    {
        return _text;
    }
}

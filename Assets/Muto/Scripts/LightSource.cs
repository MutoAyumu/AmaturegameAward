using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;

public class LightSource : MonoBehaviour
{
    [SerializeField] Light2D _light = default;
    [SerializeField] float _time = 0.5f;
    [SerializeField] float _loopTime = 10f;
    [SerializeField] bool _isOn = true;
    [SerializeField] bool _isLoop;
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
        }
        else
        {
            _light.intensity = 0;
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

                return DOVirtual.Float(_light.intensity, 0, time, value => _light.intensity = value);
            }
            else if (!_isOn)
            {
                //_isOn = true;

                return DOVirtual.Float(_light.intensity, 1.0f, time, value => _light.intensity = value);
            }
        }

        return null;
    }
}

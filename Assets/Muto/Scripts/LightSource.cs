using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;

public class LightSource : MonoBehaviour, IGhostGimic
{
    [SerializeField] Light2D _light = default;
    [SerializeField] float _time = 0.5f;
    [SerializeField] bool _isOn = true;
    [SerializeField, Tooltip("IActivateを継承したGameObjectを入れる")] GameObject[] _activate = default;

    GhostController _ghost;
    Text _lightCountText = default;

    public bool IsOn { get => _isOn; }

    /*ToDo
        
    */
    private void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        _ghost = CharacterManager.Instance.Ghost;
        _lightCountText = CharacterManager.Instance.LightCountTest;

        if (_isOn)
        {
            _light.intensity = 1;
        }
        else
        {
            _light.intensity = 0;
        }
    }
    /// <summary>
    /// ライトのオンオフ切り替えをする関数
    /// </summary>
    public void Action()
    {
        if (_light)
        {
            if (_isOn && _ghost.LightCount < _ghost.UpperLimit)
            {
                _isOn = false;
                var tween1 = DOVirtual.Float(_light.intensity, 0, _time, value => _light.intensity = value);
                var tween2 = DOVirtual.Float(_ghost.Light.intensity, 1.0f / _ghost.UpperLimit + _ghost.Light.intensity
                    , _time, value => _ghost.Light.intensity = value);

                DOTween.Sequence()
                    .OnStart(() =>
                    {
                        _ghost.IsControll = false;
                        _ghost.Stop();
                    })
                    .Append(tween1)
                    .Append(tween2)
                    .AppendCallback(() =>
                    {
                        _ghost.LightCount++;
                        _lightCountText.text = _ghost.LightCount.ToString();
                        _ghost.IsControll = true;
                    });
            }
            else if (!_isOn && 0 < _ghost.LightCount)
            {
                _isOn = true;
                var tween1 = DOVirtual.Float(_light.intensity, 1, _time, value => _light.intensity = value);
                var tween2 = DOVirtual.Float(_ghost.Light.intensity, _ghost.Light.intensity - 1.0f / _ghost.UpperLimit
                    , _time, value => _ghost.Light.intensity = value);

                DOTween.Sequence()
                    .OnStart(() =>
                    {
                        _ghost.IsControll = false;
                        _ghost.Stop();
                    })
                    .Append(tween2)
                    .Append(tween1)
                    .AppendCallback(() =>
                    {
                        _ghost.LightCount--;
                        _lightCountText.text = _ghost.LightCount.ToString();
                        _ghost.IsControll = true;

                        foreach (var go in _activate)
                        {
                            go.GetComponent<IActivate>()?.Action();
                        }
                    });
            }
        }
    }
}

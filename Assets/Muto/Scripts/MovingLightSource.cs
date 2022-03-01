using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class MovingLightSource : MonoBehaviour
{
    [SerializeField] Light2D _light = default;
    [SerializeField] float _time = 0.5f;
    [SerializeField] bool _isOn = true;
    [SerializeField, Tooltip("IActivateを継承したGameObjectを入れる")] GameObject[] _activate = default;

    private void Start()//後で直す
    {
        IsStartFalse();
    }
    void IsStartFalse()
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
    /// <summary>
    /// ライトのオンオフをする関数
    /// </summary>
    public void IsMoving()
    {
        if (_light)
        {
            if (_isOn)   //光源がある場合
            {
                DOVirtual.Float(_light.intensity, 0, _time, value => _light.intensity = value)  //dotweenでintensityを１から０にしている
                    .OnComplete(() =>
                    {
                        _isOn = false;
                        CharacterManager._instance.Ghost._lightNum++;
                    });
            }
            else
            {
                DOVirtual.Float(_light.intensity, 1, _time, value => _light.intensity = value)
                    .OnComplete(() =>
                    {
                        _isOn = true;
                        CharacterManager._instance.Ghost._lightNum--;

                        foreach (var go in _activate)
                        {
                            go.GetComponent<IActivate>()?.Action();
                        }
                    });
            }
        }
        else
        {
            Debug.LogError("2DLightがセットされていません");
        }
    }
}

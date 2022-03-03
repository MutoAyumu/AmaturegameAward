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

    public bool IsOn { get => _isOn; }

    /*ToDo
        キャストして使う
        モデル図確認
    */

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

        CharacterManager._instance.LightCountText.text = CharacterManager._instance.Ghost._lightNum.ToString();
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
                _isOn = false;
                var tween1 = DOVirtual.Float(_light.intensity, 0, _time, value => _light.intensity = value);  //dotweenでintensityを１から０にしている
                var tween2 = DOVirtual.Float(CharacterManager._instance.Ghost.Light.intensity, 1.0f / CharacterManager._instance.Ghost.UpperLimit +
                CharacterManager._instance.Ghost.Light.intensity, _time, value => CharacterManager._instance.Ghost.Light.intensity = value);
                
                DOTween.Sequence()
                    .Append(tween1)
                    .Append(tween2)
                    .AppendCallback(() =>
                    {
                        CharacterManager._instance.Ghost._lightNum++;
                        CharacterManager._instance.LightCountText.text = CharacterManager._instance.Ghost._lightNum.ToString();
                        CharacterManager._instance.Ghost.Stop();
                    });
            }
            else
            {
                _isOn = true;
                var tween1 = DOVirtual.Float(_light.intensity, 1, _time, value => _light.intensity = value);  //dotweenでintensityを１から０にしている
                var tween2 = DOVirtual.Float(CharacterManager._instance.Ghost.Light.intensity, CharacterManager._instance.Ghost.Light.intensity -
                1.0f / CharacterManager._instance.Ghost.UpperLimit, _time, value => CharacterManager._instance.Ghost.Light.intensity = value);

                DOTween.Sequence()
                    .Append(tween2)
                    .Append(tween1)
                    .AppendCallback(() =>
                    {
                        CharacterManager._instance.Ghost._lightNum--;
                        CharacterManager._instance.LightCountText.text = CharacterManager._instance.Ghost._lightNum.ToString();
                        CharacterManager._instance.Ghost.Stop();

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

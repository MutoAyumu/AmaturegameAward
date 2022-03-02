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
    [SerializeField, Tooltip("IActivate‚ğŒp³‚µ‚½GameObject‚ğ“ü‚ê‚é")] GameObject[] _activate = default;

    public bool IsOn { get => _isOn; }

    private void Start()//Œã‚Å’¼‚·
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
    /// ƒ‰ƒCƒg‚ÌƒIƒ“ƒIƒt‚ğ‚·‚éŠÖ”
    /// </summary>
    public void IsMoving()
    {
        if (_light)
        {
            if (_isOn)   //ŒõŒ¹‚ª‚ ‚éê‡
            {
                _isOn = false;
                var tween1 = DOVirtual.Float(_light.intensity, 0, _time, value => _light.intensity = value);  //dotween‚Åintensity‚ğ‚P‚©‚ç‚O‚É‚µ‚Ä‚¢‚é
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
                var tween1 = DOVirtual.Float(_light.intensity, 1, _time, value => _light.intensity = value);  //dotween‚Åintensity‚ğ‚P‚©‚ç‚O‚É‚µ‚Ä‚¢‚é
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
            Debug.LogError("2DLight‚ªƒZƒbƒg‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
        }
    }
}

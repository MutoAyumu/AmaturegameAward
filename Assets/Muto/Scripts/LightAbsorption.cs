using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class LightAbsorption : MonoBehaviour
{
    [SerializeField, Tooltip("幽霊にアタッチされているLight2Dを入れる")] Light2D _light = default;
    [SerializeField, Tooltip("幽霊が保持することが出来る光源の上限")] int _limit = 3;
    [SerializeField] float _time = 0.5f;
    int _lightCount = 0;

    public void Absorption(float h, float v, float length, LayerMask layer)
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v).normalized, length, layer);
        var other = hit.collider?.GetComponent<LightSource>();

        if (other)
        {
            if (other.IsOn && _lightCount < _limit)
            {
                DOTween.Sequence()
                    .Append(other.Action(_time))
                    .Append(DOVirtual.Float(_light.intensity, 1.0f / _limit + _light.intensity, _time, value => _light.intensity = value))
                    .OnStart(() =>
                        {
                            other.IsOn = false;
                        })
                    .OnComplete(() =>
                        {
                            _lightCount++;
                        });
            }
            else if (!other.IsOn && 0 < _lightCount)
            {
                DOTween.Sequence()
                    .Append(DOVirtual.Float(_light.intensity, _light.intensity - 1.0f / _limit, _time, value => _light.intensity = value))
                    .Append(other.Action(_time))
                    .OnStart(() =>
                    {
                        _lightCount--;
                    })
                    .OnComplete(() =>
                    {
                        other.IsOn = true;
                    });
            }
        }
    }
}

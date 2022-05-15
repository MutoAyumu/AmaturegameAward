using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeChangeColor : MonoBehaviour
{
    [SerializeField] Slider _slider = default;
    [SerializeField] Image _fillImage = default;
    [SerializeField] float _combinedValue = 3f;
    [SerializeField] Color _color1, _color2, _color3 = default;

    private void Update()
    {
        if (!_slider || !_fillImage) return;

        var c = _slider.value / _slider.maxValue;

        if (c > 0.7f)
            _fillImage.color = Color.Lerp(_color2, _color1, (c - 0.7f) * _combinedValue);
        else if(c > 0.35f)
            _fillImage.color = Color.Lerp(_color3, _color2, (c - 0.35f) * _combinedValue);
    }
}

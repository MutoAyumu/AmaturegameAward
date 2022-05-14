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
            ChangeColor(_color1, _color2, 0.7f);
        else if(c > 0.35f)
            ChangeColor(_color2, _color3, 0.35f);
    }
    void ChangeColor(Color from, Color to, float where)
    {
        var num = _slider.value / _slider.maxValue;
        _fillImage.color = Color.Lerp(to, from, (num - where) * _combinedValue);
    }
}

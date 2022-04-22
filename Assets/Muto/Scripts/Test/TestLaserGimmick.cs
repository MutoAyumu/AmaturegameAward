using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLaserGimmick : MonoBehaviour
{
    [SerializeField] LineRenderer _line = default;
    [SerializeField] float _lenght = 3f;
    [SerializeField] Vector2 _lineDir = Vector2.zero;

    private void Update()
    {
        var ray = Physics2D.Raycast(this.transform.position, _lineDir, Mathf.Infinity);
        _line.SetPosition(0, this.transform.position);
        _line.SetPosition(1, ray.point);
    }
}

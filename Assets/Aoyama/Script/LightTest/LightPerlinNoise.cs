using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPerlinNoise : MonoBehaviour
{ 
    [SerializeField]
    float _max = 0.7f;
    [SerializeField]
    float _min = 0.5f;

    Light2D _light;
    float _distance = 0;
    void Start()
    {
        _distance = _max - _min;
        _light = GetComponent<Light2D>();
    }
    // Update is called once per frame
    void Update()
    {
        _light.intensity = _min + _distance * Mathf.PerlinNoise(Time.time, 0);
    }
}

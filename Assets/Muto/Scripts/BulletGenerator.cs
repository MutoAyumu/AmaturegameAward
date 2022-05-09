using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] MarblesScript _bullet = default;
    [SerializeField] Transform _setPos = default;
    [SerializeField] float _firingSpeed = 3f;
    float _timer;

    private void Update()
    {
        Shooting();
    }
    void Shooting()
    {
        _timer += Time.deltaTime;

        if(_timer >= _firingSpeed)
        {
            _timer = 0;
            var b = Instantiate(_bullet, _setPos.position, Quaternion.identity);
            b.transform.up = _setPos.transform.up;
        }
    }
}

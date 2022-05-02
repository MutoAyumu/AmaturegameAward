using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Laser : MonoBehaviour
{
    [SerializeField] Transform _muzzle;
    [SerializeField] GameObject _rightLaser;
    [SerializeField] GameObject _leftLaser;
    [SerializeField] Rotate _rotate;

    bool _isRight = false;
    void Start()
    {
        
    }

    public void Laser()
    {
        _isRight = _rotate.IsRight;

        if (_isRight)
        {
            Instantiate(_rightLaser, _muzzle.position, Quaternion.identity, transform);
        }
        else
        {
            Instantiate(_leftLaser, _muzzle.position, Quaternion.identity, transform);
        }
    }
}

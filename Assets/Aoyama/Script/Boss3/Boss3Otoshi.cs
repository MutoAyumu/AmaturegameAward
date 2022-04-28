using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Otoshi : MonoBehaviour
{
    [SerializeField] Transform _rightMuzzle;
    [SerializeField] Transform _leftMuzzle;
    [SerializeField] float _intervalRange = 1;
    [SerializeField] float _intervalTime = 0.2f;
    [SerializeField] int _kosu = 5;
    [SerializeField] GameObject _tama;

    Vector3 _rightPosition;
    Vector3 _leftPosition;

    public void Otoshi()
    {
        StartCoroutine(OtoShiCor());
    }

    IEnumerator OtoShiCor()
    {
        for (int i = 0; i < _kosu; i++)
        {
            float right = _rightMuzzle.position.x + i * _intervalRange;
            _rightPosition = new Vector3(right, _rightMuzzle.position.y, _rightMuzzle.position.z);

            float left = _leftMuzzle.position.x - i * _intervalRange;
            _leftPosition = new Vector3(left, _leftMuzzle.position.y, _leftMuzzle.position.z);

            Instantiate(_tama, _rightPosition, Quaternion.identity);
            Instantiate(_tama, _leftPosition, Quaternion.identity);

            yield return new WaitForSeconds(_intervalTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rinpun : MonoBehaviour 
{
    [Header("ƒ}ƒYƒ‹")]
    [SerializeField] Transform _rightMuzzle;
    [SerializeField] Transform _leftMuzzle;
    [SerializeField] Transform _upMuzzle;
    [SerializeField] Transform _downMuzzle;

    [Header("’e")]
    [SerializeField] GameObject _rightBullet;
    [SerializeField] GameObject _leftBullet;
    [SerializeField] GameObject _upBullet;
    [SerializeField] GameObject _downBullet;

    public void RinpunAttack()
    {
        GameObject.Instantiate(_rightBullet, _rightMuzzle.position, Quaternion.identity);
        GameObject.Instantiate(_leftBullet, _leftMuzzle.position, Quaternion.identity);
        GameObject.Instantiate(_upBullet, _upMuzzle.position, Quaternion.identity);
        GameObject.Instantiate(_downBullet, _downMuzzle.position, Quaternion.identity);
    }
}

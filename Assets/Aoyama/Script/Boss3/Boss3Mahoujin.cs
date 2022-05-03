using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Mahoujin : MonoBehaviour
{
    [SerializeField] float _intervalRange = 1;
    [SerializeField] float _intervalTime = 0.2f;
    [SerializeField] int _kosu = 5;
    [SerializeField] GameObject _tama;
    [SerializeField] bool _test = false;
    [SerializeField] Transform _testTransform;

    Vector3 _target;
    CharacterControllerBase _player;
    CharacterControllerBase _ghost;
    Vector3 _dir;

    void Update()
    {
        if(_test)
        {
            Mahou();
            _test = false;
        }
    }

    public void Mahou()
    {
        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;
        _target = PlayerPosition();

        if (_test)
        {
            _target = _testTransform.position;
        }
        _dir = (_target - transform.position).normalized;
        Debug.Log(_dir);

        StartCoroutine(MahouCor());
    }

    Vector3 player1;
    Vector3 player2;
    /// <summary>
    /// 2êlÇÃPlayerÇÃÇ§ÇøãﬂÇ¢ÇŸÇ§ÇÃç¿ïWÇï‘Ç∑
    /// </summary>
    /// <returns></returns>
    Vector3 PlayerPosition()
    {
        player1 = _player.ColliderCenter();
        Debug.DrawLine(transform.position, player1);
        float isHit1 = Vector3.Distance(transform.position, player1);

        player2 = _ghost.ColliderCenter();
        Debug.DrawLine(transform.position, player2);
        float isHit2 = Vector3.Distance(transform.position, player2);

        if (isHit1 < isHit2)
        {
            Debug.DrawLine(transform.position, player1, Color.red);
            return player1;
        }
        else
        {
            Debug.DrawLine(transform.position, player2, Color.red);
            return player2;
        }
    }

    Vector3 _vec;
    IEnumerator MahouCor()
    {
        for (int i = 1; i <= _kosu; i++)
        {
            _vec = _dir * i;
            Instantiate(_tama, _vec + transform.position, Quaternion.identity);

            yield return new WaitForSeconds(_intervalTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTile : MonoBehaviour, IActivate
{
    [SerializeField] TileParam[] _param;
    [SerializeField] string _humanTag = "Player";
    [SerializeField] string _ghostTag = "Respawn";
    [SerializeField] GameObject _col = default;

    int _index = 0;
    Vector3 dir;
    [Tooltip("現在移動可能かどうかのフラグ")] bool _isMove = false;
    [Tooltip("現在移動可能か前に進んでいるかのフラグ")] bool _isNext = false;
    bool IsOn;

    void Start()
    {
        //_isMove = true;
        //_isNext = true;

        _col.SetActive(false);
        transform.position = _param[_index].MovePoint.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_humanTag) || collision.gameObject.CompareTag(_ghostTag))
        {
            collision.gameObject.transform.SetParent(this.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_humanTag) || collision.gameObject.CompareTag(_ghostTag))
        {
            collision.gameObject.transform.parent = null;
        }
    }

    void Update()
    {
        if (_isMove && _isNext)
        {
            MoveNext();
        }
        else if (_isMove && !_isNext)
        {
            MoveBack();
        }

        if (_index == _param.Length - 1)
        {
            _isNext = false;
        }
        else if (_index == 0)
        {
            _isNext = true;
        }
    }

    TileParam _currentParam;
    void MoveNext()
    {
        _isMove = false;
        _col.SetActive(true);
        _currentParam = _param[++_index];
        Debug.Log(_index);
        dir = _currentParam.MovePoint.position;
        this.transform
            .DOMove(dir, _currentParam.MoveTime).SetEase(Ease.InOutCubic)
            .OnComplete(() =>
            {
                Delay();
            });
    }

    void MoveBack()
    {
        _isMove = false;
        _col.SetActive(true);
        _currentParam = _param[--_index];
        Debug.Log(_index);
        dir = _currentParam.MovePoint.position;
        this.transform
            .DOMove(dir, _currentParam.MoveTime).SetEase(Ease.InOutCubic)
            .OnComplete(() =>
            {
                Delay();
            });
    }

    void Delay()
    {
        DOVirtual.DelayedCall(_currentParam.WaitTime, () => _isMove = true)
            .OnStart(() => 
            {
                if(_index == 0 || _index == _param.Length - 1)
                {
                    _col.SetActive(false);

                    if(_currentParam.Collider)
                    {
                        _currentParam.Collider.SetActive(false);
                    }
                }
            })
            .OnComplete(() =>
            {
                if (_currentParam.Collider)
                {
                    _currentParam.Collider.SetActive(true);
                }
            });
    }
    public void Action()
    {
        if(!IsOn)
        {
            IsOn = true;
            _isMove = true;
            _isNext = true;
        }
    }
}

[System.Serializable]
public class TileParam
{
    [SerializeField] public float MoveTime = 2;

    [SerializeField] public float WaitTime = 1;

    [SerializeField] public Transform MovePoint;

    [SerializeField] public GameObject Collider;
}

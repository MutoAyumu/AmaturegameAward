using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTile : MonoBehaviour, IActivate
{
    [SerializeField] TileParam[] _param;
    [SerializeField] string _humanTag = "Player";
    [SerializeField] string _ghostTag = "Respawn";

    int _index = 0;
    Vector3 dir;
    [Tooltip("���݈ړ��\���ǂ����̃t���O")] bool _isMove = false;
    [Tooltip("���݈ړ��\���O�ɐi��ł��邩�̃t���O")] bool _isNext = false;
    bool IsOn;

    void Start()
    {
        //_isMove = true;
        //_isNext = true;
        foreach(var param in _param)
        {
            SetCollider(param, true);
        }

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
                SetCollider(_currentParam, false);
            })
            .OnComplete(() =>
            {
                SetCollider(_currentParam, true);
            });
    }
    void SetCollider(TileParam tile, bool value)
    {
        if (tile.Collider && tile.TileCollider)
        {
            tile.Collider.SetActive(value);
            tile.TileCollider.SetActive(value);
        }
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

    [SerializeField] public GameObject TileCollider;
}

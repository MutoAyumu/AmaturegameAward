using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class OnOffEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("Enemyのグループ")]
    GameObject[] _enemys;
    [SerializeField]
    string _humanTag = "Player";
    [SerializeField]
    string _ghostTag = "Respawn";
    int _count = 0;
    [SerializeField] int _arrayNum;

    [SerializeField] GameObject[] _gimic = default;

    private void Start()
    {
        OffSetActive();
        _count = _enemys.Length;
        Array.ForEach(_enemys, en => en.GetComponent<EnemyDamage>()?.SetNumber(_arrayNum));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(_enemys == null)
        {
            Destroy(gameObject);
        }
        Debug.Log(collision);
        if (collision.CompareTag(_humanTag) || collision.CompareTag(_ghostTag))
        {
            OnSetActive();
            Activate();
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(_humanTag) || collision.CompareTag(_ghostTag))
    //    {
    //        OffSetActive();
    //    }
    //}

    void OnSetActive()
    {
        Array.ForEach(_enemys, go => go.SetActive(true));
    }

    void OffSetActive()
    {
        Array.ForEach(_enemys, go => go.SetActive(false));
    }
    public void Decrease()
    {
        _count--;
        Debug.Log("数が減った");

        if(_count <= 0)
        {
            //ここで扉を開く
            Debug.Log("みんな死んだ!!");
            Activate();
        }
    }
    void Activate()
    {
        foreach(var go in _gimic)
        {
            go.GetComponent<IActivate>()?.Action();
        }
    }
}

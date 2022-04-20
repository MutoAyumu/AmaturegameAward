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

    private void Start()
    {
        OffSetActive();
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
}

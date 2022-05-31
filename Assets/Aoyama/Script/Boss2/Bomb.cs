using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("‰¹")]
    [SerializeField] string _cueName = "KuroBossBomb1";
    [SerializeField] int _damage = 2;

    private void Start()
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cueName);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>() != null)
        {
            collision.gameObject.GetComponent<PlayerHP>().Damage(_damage);
        }
    }
}

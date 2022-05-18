using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField, Tooltip("‰ñ•œ—Ê")]
    int _healValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPalam.Instance.LifeChange(_healValue);
            CharacterManager.Instance.UIHPUpdate(PlayerPalam.Instance.Life);
            //SoundManager.Instance.CriAtomPlay(CueSheet.SE,"");
            Destroy(this.gameObject);
        }
    }
}

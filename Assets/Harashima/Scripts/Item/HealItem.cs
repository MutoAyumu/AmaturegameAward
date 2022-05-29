using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    [SerializeField, Tooltip("‰ñ•œ‚·‚é’l")]
    int _healValue = 1;

    [SerializeField]
    string _cueName = "ItemCure";

    public override void Use()
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE,_cueName);
        PlayerPalam.Instance.LifeChange(_healValue);
        CharacterManager.Instance.UIHPUpdate(PlayerPalam.Instance.Life);
        ItemManager.Instance.ItemValueChange(_itemID,-1);
    }
}

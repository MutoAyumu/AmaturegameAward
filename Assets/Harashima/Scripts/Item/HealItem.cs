using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    [SerializeField, Tooltip("�񕜂���l")]
    int _healValue = 1;

    public override void Use()
    {
        PlayerPalam.Instance.LifeChange(_healValue);
        CharacterManager.Instance.UIHPUpdate(PlayerPalam.Instance.Life);
        ItemManager.Instance.ItemValueChange(_itemID,-1);
    }
}

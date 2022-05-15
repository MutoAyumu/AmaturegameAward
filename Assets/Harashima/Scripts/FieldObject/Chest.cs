using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IActivate,ISetText
{
    [SerializeField, Tooltip("ドロップするアイテム")]
    int _dropItemIndex;

    [SerializeField, Tooltip("ドロップする数")]
    int _dropValue  =1;

    [SerializeField, Tooltip("このオブジェクトのアニメーターコンポーネント")]
    Animator _animator;

    [SerializeField, Tooltip("アイテムを使う時のSE")]
    AudioClip[] _audios;

    [SerializeField] string _text = "B 宝箱を開ける";

    /// <summary>この宝箱が既に開いているかを判定するフラグ</summary>
    bool isOpen = false;

    public void Action()
    {
        if (!isOpen)
        {
            ItemManager.Instance.ItemValueChange(_dropItemIndex, _dropValue);//アイテム獲得                                                                             
            SoundManager.Instance.SoundPlay(_audios[0]);//音鳴らす
            _animator.SetTrigger("Open");
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "OpenChest");
            isOpen = true; //開いたのでフラグをTrueに
        }
    }
    public string SetText()
    {
        return _text;
    }
}

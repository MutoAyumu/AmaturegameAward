using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour,IActivate,ISetText
{
    [SerializeField, Tooltip("���X�|�[������ꏊ")]
    Transform[] _respawnArray;
    [SerializeField] string _text = "�Z�[�u�|�C���g��ݒu����";
    public void Action()
    {
        if(_respawnArray.Length>=2)
        {
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SystemText");
            //CharacterManager.Instance.SetResetPos(_respawnArray[0].position, _respawnArray[1].position);
            CharacterManager.Instance.SetResetPos();
        }     
    }
    public string SetText()
    {
        return _text;
    }
}

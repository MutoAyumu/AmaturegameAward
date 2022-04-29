using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cage : MonoBehaviour, IDamage
{
    [SerializeField,Tooltip("�^�C�����C���𐧌䂷��N���X")]
    PlayableDirector _director;

    public void Damage(int damage)
    {
        //�v���C���[�����Ȃ��悤��
        CharacterManager.Instance.Human.Stop();
        CharacterManager.Instance.Ghost.Stop();
        //�v���C���[�؂�ւ�����悤��
        CharacterManager.Instance.Switching();
        //�^�C�����C���Đ�
        _director.Play();
        Debug.Log("a");
    }
}

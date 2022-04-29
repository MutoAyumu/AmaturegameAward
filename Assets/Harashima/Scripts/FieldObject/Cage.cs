using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cage : MonoBehaviour, IDamage
{
    [SerializeField,Tooltip("タイムラインを制御するクラス")]
    PlayableDirector _director;

    public void Damage(int damage)
    {
        //プレイヤー動けないように
        CharacterManager.Instance.Human.Stop();
        CharacterManager.Instance.Ghost.Stop();
        //プレイヤー切り替えられるように
        CharacterManager.Instance.Switching();
        //タイムライン再生
        _director.Play();
        Debug.Log("a");
    }
}

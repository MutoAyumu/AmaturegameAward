using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPipeScript : MonoBehaviour
{
    [SerializeField] ParticleSystem _system = default;
    [SerializeField] Collider2D _col = default;

    bool IsParticle;

    private void Start()
    {
        _col.gameObject.SetActive(true);
    }
    private void Update()
    {
        if(!IsParticle)
        {
            if(_system.particleCount > 10)  //マジックナンバーを直しておく
            {
                IsParticle = true;
            }
        }

        if(_system && IsParticle)
        {
            if(_system.particleCount < 5)
            {
                _col.gameObject.SetActive(false);
            }
        }
    }
    void OnParticleCollision(GameObject go)
    {
        if(go.CompareTag("Player"))
        //ダメージを与える
        go.GetComponent<PlayerHP>()?.Damage();
    }
}

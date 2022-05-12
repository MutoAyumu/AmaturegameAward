using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SteamPipeScript : MonoBehaviour, IActivate
{
    [SerializeField] ParticleSystem _system = default;
    [SerializeField] Collider2D _col = default;
    [SerializeField] CinemachineImpulseSource _source = default;
    [SerializeField] Vector2 _dir = default;
    [SerializeField] LayerMask _mask;

    [SerializeField] bool IsActive = default;
    bool IsParticle;

    private void Start()
    {
        if(IsActive)
        {
            _system.Play();
            _col.gameObject.SetActive(true);
        }
        else
        {
            _system.Stop();
            _col.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        var hit = Physics2D.Raycast(this.transform.position, _dir, _dir.magnitude, _mask);

        if (_system.isPlaying)
        {
            if (hit.collider?.GetComponent<MoveBlock>())
            {
                if (_col.gameObject.activeSelf)
                {
                    _col.gameObject.SetActive(false);
                }
            }
            else
            {
                if (!_col.gameObject.activeSelf)
                {
                    _col.gameObject.SetActive(true);
                }
            }
        }

        //if(!IsParticle)
        //{
        //    if(_system.particleCount > 10)  //マジックナンバーを直しておく
        //    {
        //        IsParticle = true;
        //    }
        //}

        //if(_system && IsParticle)
        //{
        //    if(_system.particleCount < 5)
        //    {
        //        _col.gameObject.SetActive(false);
        //    }
        //}
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, _dir);
    }
    void OnParticleCollision(GameObject go)
    {
        if (go.CompareTag("Player"))
        {
            //ダメージを与える
            var p = go.GetComponent<PlayerHP>();
            p.Damage();
        }
    }
    public void Action()
    {
        if(IsActive)  //消す
        {
            IsParticle = false;
            IsActive = false;
            _system.Stop();
            _system.Clear();
        }
        else    //付ける
        {
            IsParticle = true;
            IsActive = true;
            _system.Play();
        }

        _col.gameObject.SetActive(IsParticle);
    }
}

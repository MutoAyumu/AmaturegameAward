using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Melee : MonoBehaviour
{
    [SerializeField] float _power = 5;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] bool _test = false;
    [SerializeField] Transform _testTransform;
    [Header("âπ")]
    [SerializeField] string _cueName = "KuroBossVoice";
    [SerializeField] int _damage = 3;

    Vector3 _target;
    CharacterControllerBase _player;
    CharacterControllerBase _ghost;
    bool _isAttack = false;

    public void Melee()
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cueName);

        _isAttack = true;
        _target = PlayerPosition();

        if(_test)
        {
            _target = _testTransform.position;
        }
        _target = (_target - transform.position).normalized * _power;
        _rb.AddForce(_target, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isAttack == true && collision.gameObject.GetComponent<PlayerHP>() != null)
        {
            collision.gameObject.GetComponent<PlayerHP>().Damage(_damage);
        }
    }

    public void AttackReset()
    {
        _isAttack = false;
    }

    Vector3 player1;
    Vector3 player2;
    /// <summary>
    /// 2êlÇÃPlayerÇÃÇ§ÇøãﬂÇ¢ÇŸÇ§ÇÃç¿ïWÇï‘Ç∑
    /// </summary>
    /// <returns></returns>
    Vector3 PlayerPosition()
    {
        _player = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;

        player1 = _player.ColliderCenter();
        Debug.DrawLine(transform.position, player1);
        float isHit1 = Vector3.Distance(transform.position, player1);

        player2 = _ghost.ColliderCenter();
        Debug.DrawLine(transform.position, player2);
        float isHit2 = Vector3.Distance(transform.position, player2);

        if (isHit1 < isHit2)
        {
            Debug.DrawLine(transform.position, player1, Color.red);
            return player1;
        }
        else
        {
            Debug.DrawLine(transform.position, player2, Color.red);
            return player2;
        }
    }
}

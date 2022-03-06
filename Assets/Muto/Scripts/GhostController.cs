using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : CharacterControllerBase
{
    bool _isFixedRange = default;

    public bool IsFixedRange { get => _isFixedRange; set => _isFixedRange = value; }

    public override void OnUpdate()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !_isFixedRange)
        {
            _isFixedRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isFixedRange = false;
        }
    }
}

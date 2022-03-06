using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : CharacterControllerBase
{
    bool together = default;

    public bool Together { get => together; set => together = value; }

    public override void OnUpdate()
    {
        
    }
}

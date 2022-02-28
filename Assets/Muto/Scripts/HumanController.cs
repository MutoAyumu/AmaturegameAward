using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] Transform _ghostMovePos = default;
    public Transform GhostMovePos { get => _ghostMovePos; }
}

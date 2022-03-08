using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour, IHumanGimic
{
    [SerializeField] Rigidbody2D _rb = default;

    public Rigidbody2D Rb { get => _rb;}

    public void Action()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour,ISetText
{
    [SerializeField] Rigidbody2D _rb = default;
    [SerializeField] GameObject[] _activate = default;
    [SerializeField] string _text = "B@” ‚ð‰Ÿ‚·";

    public Rigidbody2D Rb { get => _rb;}

    public void Action()
    {
        foreach(var go in _activate)
        {
            go.GetComponent<IActivate>()?.Action();
        }
    }
    public string SetText()
    {
        return _text;
    }
}

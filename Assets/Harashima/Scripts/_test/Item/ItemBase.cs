using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField]protected int _itemID;

    public abstract void Use();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

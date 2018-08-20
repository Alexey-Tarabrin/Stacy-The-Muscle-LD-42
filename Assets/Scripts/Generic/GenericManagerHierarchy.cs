using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericManageableClass<T> : MonoBehaviour where T : Component
{
    protected T _manager;

    public void SetManager(T manager)
    {
        _manager = manager;
    }
}
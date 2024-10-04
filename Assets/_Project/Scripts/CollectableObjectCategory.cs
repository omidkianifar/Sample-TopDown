using System;
using UnityEngine;
using VContainer;

namespace _Project.Scripts
{
    [Flags]
    public enum CollectableObjectCategory 
    {
        None = 0,
        Fruits = 1,
        Foods = 2,
        Tools = 4,
    }
}

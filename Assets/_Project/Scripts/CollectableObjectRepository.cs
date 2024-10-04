using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "CollectableObjectRepository", menuName = "_Project/Collectable/Create CollectableObject Repository")]
    public class CollectableObjectRepository : ScriptableObject 
    {
        [SerializeField] CollectableObject[] _objects;


        public int ObjectsCount => _objects.Length;
        public IEnumerable<CollectableObject> Objects => _objects.AsReadOnlyCollection();
    }
}

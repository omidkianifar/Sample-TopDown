using UnityEngine;

namespace _Project.Scripts
{
    public class CollectableObjectController : MonoBehaviour
    {
        [SerializeField] CollectableObject _data;

        public CollectableObject Data => _data;

        
        public void Collect() { }
    }
}
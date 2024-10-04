using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "CollectableObject", menuName = "_Project/Collectable/Create New CollectableData")]
    public class CollectableObject : ScriptableObject
    {
        [SerializeField] string _name;
        [SerializeField] CollectableObjectCategory _category;
        [SerializeField] string _description;
        [SerializeField] int _size;
        [SerializeField] Sprite _icon;

        public string Name => _name;
        public CollectableObjectCategory Category => _category;
        public string Description => _description;
        public int Size => _size;
        public Sprite Icon => _icon;
    }
}

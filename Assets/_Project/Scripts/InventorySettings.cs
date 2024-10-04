using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "InventorySettings", menuName = "_Project/Settings/Create InventorySettings")]
    public class InventorySettings : ScriptableObject 
    {
        [SerializeField] int _slots;
        [SerializeField] int _size;

        public int slots => _slots;
        public int size => _size;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace _Project.Scripts
{
    public class InventoryView : MonoBehaviour 
    {
        [SerializeField] SlotView _slotPrefab;
        [SerializeField] Transform _slotsParent;

        private List<SlotView> _slots = new();

        [Inject] CollectableObjectInventory _inventory;

        private void Start()
        {
            for (int i = 0; i < _inventory.SlotsCount; i++) 
            {
                var slot = Instantiate(_slotPrefab, _slotsParent);

                var item = _inventory[i];

                slot.Initialize(item);

                _slots.Add(slot);
            }
        }
    }
}
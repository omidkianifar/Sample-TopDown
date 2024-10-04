using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class CollectableObjectInventory
    {
        public int SlotsCount => _settings.slots;
        public int SlotCapacity => _settings.size;

        private readonly List<InventorySlot> _slots;
        private readonly InventorySettings _settings;

        public CollectableObjectInventory()
        {
            _settings = Resources.Load<InventorySettings>(nameof(InventorySettings));

            _slots = new List<InventorySlot>(_settings.slots);

            for (int i = 0; i < SlotsCount; i++)
            {
                _slots.Add(new InventorySlot(SlotCapacity));
            }
        }

        public bool AddItem(CollectableObject item)
        {
            if (item == null || item.Size <= 0)
            {
                return false;
            }

            foreach (var slot in _slots)
            {
                if (slot.TryAddItem(item))
                {
                    Debug.Log($"Added {item.Name} to slot {slot.Index}");

                    return true;
                }
            }

            Debug.Log("Not enough capacity in any slot.");
            return false;
        }

        public bool RemoveItem(CollectableObject item)
        {
            if (item == null)
            {
                return false;
            }

            foreach (var slot in _slots)
            {
                if (slot.RemoveItem(item))
                {
                    Debug.Log($"Removed {item.Name} from slot {slot.Index}");

                    return true;
                }
            }

            Debug.Log($"Item {item.Name} not found in any slot.");
            return false;
        }

        public bool RemoveAllItems(CollectableObject item)
        {
            if (item == null)
            {
                return false;
            }

            bool itemRemoved = false;
            foreach (var slot in _slots)
            {
                if (slot.RemoveAllItems(item))
                {
                    itemRemoved = true;
                    Debug.Log($"Removed all {item.Name} from slot {slot.Index}");
                }
            }

            if (!itemRemoved)
            {
                Debug.Log($"No items of {item.Name} found to remove.");
            }

            return itemRemoved;
        }

        public bool Contains(CollectableObject item)
        {
            if (item == null)
            {
                return false;
            }

            foreach (var slot in _slots)
            {
                if (slot.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public InventorySlot this[int index]
        {
            get
            {
                if (index < 0 || index >= _slots.Count)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                }

                return _slots[index];
            }
        }

        public void PrintInventory()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                Debug.Log($"Slot {i}: {(_slots[i].IsEmpty ? "Empty" : _slots[i].GetItemInfo())}");
            }
        }
    }
}


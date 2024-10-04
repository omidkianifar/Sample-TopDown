using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [System.Serializable]
    public class InventorySlot
    {
        private readonly int _capacity;
        private readonly List<CollectableObject> _items;

        private int _remainingCapacity;

        public int Index { get; private set; } 
        public bool IsEmpty => _items.Count == 0;
        public int ContentsCount => _items.Count;
        public CollectableObject Content => _items.FirstOrDefault();

        public event Action OnSlotUpdated;

        public InventorySlot(int capacity)
        {
            _capacity = capacity;
            _items = new List<CollectableObject>();

            _remainingCapacity = capacity;
        }

        public bool TryAddItem(CollectableObject item)
        {
            if (_remainingCapacity < item.Size)
            {
                return false; // Not enough capacity in this slot
            }

            if (!IsEmpty && Content != item)
            {
                return false; // type not match
            }

            _items.Add(item);
            _remainingCapacity -= item.Size;

            OnSlotUpdated?.Invoke();

            return true;
        }

        public bool RemoveItem(CollectableObject item)
        {
            if (_items.Remove(item))
            {
                _remainingCapacity += item.Size; // Restore capacity

                OnSlotUpdated?.Invoke();

                return true;
            }

            return false; // Item not found in this slot
        }

        public bool RemoveAllItems(CollectableObject item)
        {
            int initialCount = _items.Count;
            _items.RemoveAll(i => i.Equals(item));
            int itemsRemoved = initialCount - _items.Count;

            if (itemsRemoved > 0)
            {
                _remainingCapacity = _capacity; // Restore capacity for each removed item
                return true;
            }

            return false; // No items removed
        }

        public bool Contains(CollectableObject item)
        {
            return _items.Contains(item);
        }

        public string GetItemInfo()
        {
            string info = "";
            foreach (var item in _items)
            {
                info += $"{item.Name} (Size: {item.Size}) ";
            }
            return info.Trim();
        }

        public CollectableObject GetFirstItem()
        {
            return _items.Count > 0 ? _items[0] : null;
        }
    }
}


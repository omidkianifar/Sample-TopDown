using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class SlotView : MonoBehaviour 
    {
        [SerializeField] Image _contentImage;
        [SerializeField] TextMeshProUGUI _amountText;

        private InventorySlot _slot;

        public void Initialize(InventorySlot slot) 
        {
            _slot = slot;
            _slot.OnSlotUpdated += UpdateSlot;
            
            UpdateSlot();
        }

        private void OnDestroy()
        {
            _slot.OnSlotUpdated -= UpdateSlot;
        }

        public void UpdateSlot() 
        {
            if (_slot.IsEmpty)
            {
                _contentImage.sprite = null;
                _amountText.SetText("");
            }
            else
            {
                _contentImage.sprite = _slot.Content.Icon;
                _amountText.SetText(_slot.ContentsCount.ToString());
            }
        }
    }
}
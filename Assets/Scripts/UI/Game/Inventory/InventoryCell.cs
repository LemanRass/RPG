using Data.Items;
using Interfaces;
using Inventory;
using Inventory.Data;
using TMPro;
using UI.Tools.UIDragDrop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Game
{
    public class InventoryCell : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Button _cellBtn;
        [SerializeField] private Image _itemImg;
        [SerializeField] private TextMeshProUGUI _itemCount;

        public InventorySlot inventorySlot { get; private set; }
        public ItemData inventoryItem => inventorySlot.item;

        public void Init(InventorySlot slot)
        {
            inventorySlot = slot;
            inventorySlot.onChanged += InventorySlotOnChanged;
            
            _cellBtn.onClick.AddListener(OnCellClick);
            
            Refresh();
        }

        private void Refresh()
        {
            if (inventorySlot.isEmpty)
            {
                _itemImg.sprite = null;
                _itemImg.gameObject.SetActive(false);

                _itemCount.text = string.Empty;
                _itemCount.gameObject.SetActive(false);
            }
            else
            {
                _itemImg.sprite = inventoryItem.config.icon;
                _itemImg.gameObject.SetActive(true);

                if (inventoryItem is ICountableItem countableItem)
                {
                    _itemCount.gameObject.SetActive(true);
                    _itemCount.text = countableItem.count.ToString();
                }
                else
                {
                    _itemCount.text = string.Empty;
                    _itemCount.gameObject.SetActive(false);
                }
            }
        }
        
        private void InventorySlotOnChanged(ItemData item)
        {
            Refresh();
        }

        private void OnCellClick()
        {
            GameManager.instance.playerUnit.UseInventorySlot(inventorySlot);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            var inventoryCellDrag = eventData.pointerDrag.GetComponent<InventoryCellDrag>();
            if (inventoryCellDrag != null)
            {
                var fromSlot = inventoryCellDrag.inventoryCell.inventorySlot;
                var toSlot = inventorySlot;
                
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameManager.instance.playerUnit.SplitInventorySlot(fromSlot, toSlot, 8);
                }
                else
                {
                    GameManager.instance.playerUnit.DropInventorySlotIntoInventorySlot(fromSlot, toSlot);    
                }
            }

            var equipmentCellDrag = eventData.pointerDrag.GetComponent<EquipmentCellDrag>();
            if (equipmentCellDrag != null)
            {
                var fromSlot = equipmentCellDrag.equipmentCell.equipmentSlot;
                var toSlot = inventorySlot;
                GameManager.instance.playerUnit.DropEquipmentSlotIntoInventorySlot(fromSlot, toSlot);
            }
        }
    }
}
using Configs.Items.Core;
using Inventory;
using TMPro;
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
        public Item item => inventorySlot.item;

        public void Init(InventorySlot inventorySlot)
        {
            this.inventorySlot = inventorySlot;
            this.inventorySlot.onChanged += InventorySlotOnChanged;
            
            _cellBtn.onClick.AddListener(OnCellClick);
            
            Refresh();
        }

        private void Refresh()
        {
            _itemImg.sprite = item == null ? null : item.icon;
            _itemImg.gameObject.SetActive(item != null);

            if (item is ICountable countable)
            {
                _itemCount.gameObject.SetActive(true);
                _itemCount.text = countable.count.ToString();
            }
            else
            {
                _itemCount.gameObject.SetActive(false);
            }
        }
        
        private void InventorySlotOnChanged(Item item)
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
                GameManager.instance.playerUnit.DropInventorySlotIntoInventorySlot(fromSlot, toSlot);
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
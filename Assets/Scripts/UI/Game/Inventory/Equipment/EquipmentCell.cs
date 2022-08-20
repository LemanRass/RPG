using Configs;
using Data.Items;
using Equipment;
using Inventory.Data;
using TMPro;
using UI.Tools.UIDragDrop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Game.Inventory.Equipment
{
    public class EquipmentCell : MonoBehaviour, IDropHandler
    {
        public EquipmentType equipmentType;
        [SerializeField] private Button _cellBtn;
        [SerializeField] private Image _itemImg;
        [SerializeField] private TextMeshProUGUI _itemCount;

        public EquipmentSlot equipmentSlot { get; private set; }
        public EquipmentItemData equipmentItem => equipmentSlot.equipmentItem;

        
        public void Init(EquipmentSlot slot)
        {
            equipmentSlot = slot;
            equipmentSlot.onChanged += OnEquipmentSlotChanged;
            _cellBtn.onClick.AddListener(OnEquipmentCellClick);
            
            Refresh();
        }

        private void Refresh()
        {
            if (equipmentSlot.isEmpty)
            {
                _cellBtn.interactable = false;
                _itemImg.sprite = null;
                _itemImg.gameObject.SetActive(false);
                _itemCount.text = string.Empty;
                _itemCount.gameObject.SetActive(false);
            }
            else
            {
                _cellBtn.interactable = true;
                _itemImg.sprite = equipmentItem.config.icon;
                _itemImg.gameObject.SetActive(true);
                
                if (equipmentItem.config.maxCount > 1)
                {
                    _itemCount.gameObject.SetActive(true);
                    _itemCount.text = equipmentItem.count.ToString();
                }
                else
                {
                    _itemCount.text = string.Empty;
                    _itemCount.gameObject.SetActive(false);
                }
            }
        }

        private void OnEquipmentSlotChanged(EquipmentItemData itemConfig)
        {
            Refresh();
        }
        
        private void OnEquipmentCellClick()
        {
            GameManager.instance.playerUnit.UseEquipmentSlot(equipmentSlot);
        }

        public void OnDrop(PointerEventData eventData)
        {
            var inventoryCellDrop = eventData.pointerDrag.GetComponent<InventoryCellDrag>();
            if (inventoryCellDrop != null)
            {
                var fromSlot = inventoryCellDrop.inventoryCell.inventorySlot;
                var toSlot = equipmentSlot;

                GameManager.instance.playerUnit.DropInventorySlotIntoEquipmentSlot(fromSlot, toSlot);
            }
        }
    }
}
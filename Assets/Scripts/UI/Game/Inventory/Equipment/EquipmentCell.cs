using Configs;
using Configs.Items.Core;
using Equipment;
using TMPro;
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
        public IEquipment item => equipmentSlot.item;

        
        public void Init(EquipmentSlot equipmentSlot)
        {
            this.equipmentSlot = equipmentSlot;
            this.equipmentSlot.onChanged += OnEquipmentSlotChanged;
            _cellBtn.onClick.AddListener(OnEquipmentCellClick);
            
            Refresh();
        }

        private void Refresh()
        {
            _cellBtn.interactable = !equipmentSlot.isEmpty;
            _itemImg.gameObject.SetActive(!equipmentSlot.isEmpty);
            _itemImg.sprite = equipmentSlot.isEmpty ? null : ((Item)equipmentSlot.item).icon;

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

        private void OnEquipmentSlotChanged(IEquipment item)
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
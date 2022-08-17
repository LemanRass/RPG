using System;
using Data.Items;
using Inventory.Data;
using UnityEngine;

namespace Equipment
{
    public class EquipmentSlot : MonoBehaviour
    {
        public EquipmentType equipmentType;
        public EquipmentItemData equipmentItem { get; private set; }
        public bool isEmpty => equipmentItem == null;

        private GameObject _itemGameObject;
        
        public event Action<EquipmentItemData> onChanged;

        public void Insert(EquipmentItemData item)
        {
            Clear();
            equipmentItem = item;

            if (item != null && item.config.onUnitPrefab != null)
            {
                _itemGameObject = Instantiate(item.config.onUnitPrefab, transform);
                _itemGameObject.transform.localPosition = Vector3.zero;
                _itemGameObject.transform.localRotation = Quaternion.identity;
                _itemGameObject.transform.localScale = Vector3.one;
            }
            
            onChanged?.Invoke(item);
        }
        
        public void Clear()
        {
            if (_itemGameObject != null)
            {
                Destroy(_itemGameObject);
            }

            equipmentItem = null;
            onChanged?.Invoke(equipmentItem);
        }
    }
}
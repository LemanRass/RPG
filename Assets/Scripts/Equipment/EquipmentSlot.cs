using System;
using Configs.Items.Core;
using UnityEngine;

namespace Equipment
{
    public class EquipmentSlot : MonoBehaviour
    {
        public EquipmentType equipmentType;
        public IEquipment item;
        private GameObject _itemGameObject;
        public bool isEmpty => item == null;

        public event Action<IEquipment> onChanged;

        public void Insert(IEquipment item)
        {
            this.item = item;

            if (item != null && item.onUnitPrefab != null)
            {
                _itemGameObject = Instantiate(item.onUnitPrefab, transform);
                _itemGameObject.transform.localPosition = Vector3.zero;
                _itemGameObject.transform.localRotation = Quaternion.identity;
                _itemGameObject.transform.localScale = Vector3.one;
            }
            
            onChanged?.Invoke(item);
        }
        
        public void Clear()
        {
            Destroy(_itemGameObject);
            item = null;
            
            onChanged?.Invoke(item);
        }
    }
}
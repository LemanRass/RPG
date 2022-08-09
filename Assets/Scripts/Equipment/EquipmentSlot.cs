using System;
using Configs.Items;
using Inventory;
using UnityEngine;

namespace Equipment
{
    [Serializable]
    public class EquipmentSlot
    {
        public EquipmentType type;
        public Transform transform;
        public Item item;
        public bool isEmpty => item == null;

        public void Insert(Item item)
        {
            this.item = item;

            var itemGo = GameObject.Instantiate(item.onUnitPrefab, transform);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localRotation = Quaternion.identity;
            itemGo.transform.localScale = Vector3.one;
        }
        
        public void Clear()
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            item = null;
        }
    }
}
using Configs.Items;
using UnityEngine;

namespace Equipment
{
    public class EquipmentSlot : MonoBehaviour
    {
        public EquipmentType equipmentType;
        public Item item;
        public bool isEmpty => item == null;

        public void Insert(Item item)
        {
            this.item = item;

            var itemGo = Instantiate(item.onUnitPrefab, transform);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.transform.localRotation = Quaternion.identity;
            itemGo.transform.localScale = Vector3.one;
        }
        
        public void Clear()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            item = null;
        }
    }
}
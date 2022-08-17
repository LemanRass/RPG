using System.Collections.Generic;
using Data.Items;
using Equipment;
using UnityEngine;

namespace Configs.Items
{
    [CreateAssetMenu(fileName = "EquipmentItemConfig", menuName = "Unit/Items/EquipmentItemConfig")]
    public class EquipmentItemConfig : ItemConfig
    {
        public GameObject onUnitPrefab;
        public EquipmentType equipmentType;
        public List<ItemStat> stats;

        public override ItemData CreateInstance()
        {
            return new EquipmentItemData(this);
        }
    }
}
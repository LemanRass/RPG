using System.Collections.Generic;
using Equipment;
using Inventory.Data;
using UnityEngine;

namespace Configs.Items.Core
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
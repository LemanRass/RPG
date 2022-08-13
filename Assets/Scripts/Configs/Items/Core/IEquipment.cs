using System.Collections.Generic;
using Equipment;
using UnityEngine;

namespace Configs.Items.Core
{
    public interface IEquipment
    {
        public EquipmentType equipmentType { get; }
        public GameObject onUnitPrefab { get; }
        public List<ItemStat> stats { get; }
    }
}
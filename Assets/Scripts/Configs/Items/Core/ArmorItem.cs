using System.Collections.Generic;
using Equipment;
using UnityEngine;

namespace Configs.Items.Core
{
    public class ArmorItem : Item, IEquipment
    {
        [SerializeField] private EquipmentType _equipmentType;
        public EquipmentType equipmentType => _equipmentType;


        [SerializeField] private GameObject _onUnitPrefab;
        public GameObject onUnitPrefab => _onUnitPrefab;


        [SerializeField] private List<ItemStat> _stats;
        public List<ItemStat> stats => _stats;
    }
}
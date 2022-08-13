using System.Collections.Generic;
using Equipment;
using UnityEngine;

namespace Configs.Items.Core
{
    public class WeaponItem : Item, IEquipment
    {
        [SerializeField] private GameObject _onUnitPrefab;
        public GameObject onUnitPrefab => _onUnitPrefab;


        [SerializeField] private EquipmentType _equipmentType;
        public EquipmentType equipmentType => _equipmentType;
        
        
        [SerializeField] private List<ItemStat> _stats;
        public List<ItemStat> stats => _stats;
    }
}
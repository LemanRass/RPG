using Equipment;
using UnityEngine;

namespace Configs.Items
{
    public abstract class Item : ScriptableObject
    {
        public EquipmentType type;
        public GameObject onUnitPrefab;
        public GameObject onGroundPrefab;
    }
}
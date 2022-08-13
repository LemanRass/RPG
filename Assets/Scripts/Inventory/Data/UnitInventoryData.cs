using System.Collections.Generic;
using Configs.Items;
using Configs.Items.Core;
using UnityEngine;

namespace Inventory.Data
{
    [CreateAssetMenu(fileName = "UnitInventoryData", menuName = "Unit/Data/UnitInventoryData")]
    public class UnitInventoryData : ScriptableObject
    {
        public List<Item> items;
    }
}
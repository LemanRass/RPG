using Inventory;
using UnityEngine;

namespace Configs.Items
{
    [CreateAssetMenu(fileName = "CrimsonSword", menuName = "Unit/Items/CrimsonSwordConfig")]
    public class CrimsonSwordConfig : Item
    {
        public int minDamage;
        public int maxDamage;
    }
}
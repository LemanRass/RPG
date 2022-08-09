using UnityEngine;

namespace Configs.Items
{
    [CreateAssetMenu(fileName = "BasicSwordConfig", menuName = "Unit/Items/BasicSwordConfig")]
    public class BasicSwordConfig : Item
    {
        public int minDamage;
        public int maxDamage;
    }
}
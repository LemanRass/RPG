using Inventory.Data;
using UnityEngine;

namespace Configs.Items.Core
{
    [CreateAssetMenu(fileName = "ResourceItemConfig", menuName = "Unit/Items/ResourceItemConfig")]
    public class ResourceItemConfig : ItemConfig
    {
        public override ItemData CreateInstance()
        {
            return new ResourceItemData(this);
        }
    }
}
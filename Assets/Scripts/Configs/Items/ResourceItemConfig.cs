using Data.Items;
using UnityEngine;

namespace Configs.Items
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
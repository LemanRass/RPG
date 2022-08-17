using Data.Items;
using Effects.Core;
using UnityEngine;

namespace Configs.Items
{
    [CreateAssetMenu(fileName = "RemedyItemConfig", menuName = "Unit/Items/RemedyItemConfig")]
    public class RemedyItemConfig : ItemConfig
    {
        public Effect effect;
        public int effectLevel;
        
        public override ItemData CreateInstance()
        {
            return new RemedyItemData(this);
        }
    }
}
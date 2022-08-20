using Data.Items;
using Enums;
using UnityEngine;

namespace Configs.Items
{
    [CreateAssetMenu(fileName = "RemedyItemConfig", menuName = "Unit/Items/RemedyItemConfig")]
    public class RemedyItemConfig : ItemConfig
    {
        public EffectType effectType;
        public int effectLevel;
        
        public override ItemData CreateInstance()
        {
            return new RemedyItemData(this);
        }
    }
}
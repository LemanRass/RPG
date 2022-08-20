using Data.Effects;
using Effects;
using UnityEngine;

namespace Configs.Effects
{
    [CreateAssetMenu(fileName = "SmallManaPotionConfig", menuName = "Unit/Effects/SmallManaPotionConfig")]
    public class SmallManaPotionConfig : EffectConfig
    {
        public int mana;
        
        public override Effect CreateInstance()
        {
            return new SmallManaPotionEffect(this);
        }
    }
}
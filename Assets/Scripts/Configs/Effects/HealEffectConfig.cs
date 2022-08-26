using Data.Effects;
using UnityEngine;

namespace Configs.Effects
{
    [CreateAssetMenu(fileName = "HealEffectConfig", menuName = "Unit/Effects/HealEffectConfig")]
    public class HealEffectConfig : EffectConfig
    {
        public int amountToHeal; 
        
        public override Effect CreateInstance()
        {
            return new HealEffect(this);
        }
    }
}
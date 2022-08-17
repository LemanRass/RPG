using System;
using System.Collections.Generic;
using Effects;
using Effects.Core;
using UnityEngine;

namespace Configs.Effects
{
    [Serializable]
    public class AcidEffectLevel
    {
        public float duration;
        public float interval;
        public float damage;
    }
    
    [CreateAssetMenu(fileName = "AcidEffect", menuName = "Unit/Effects/AcidEffect")]
    public class AcidEffectConfig : EffectConfig
    {
        public List<AcidEffectLevel> levels;
        
        public override Effect CreateInstance()
        {
            return new AcidEffect(this);
        }
    }
}
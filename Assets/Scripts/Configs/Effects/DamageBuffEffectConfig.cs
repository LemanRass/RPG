using System;
using System.Collections.Generic;
using Data.Effects;
using Enums;
using UnityEngine;

namespace Configs.Effects
{
    [Serializable]
    public class DamageBuffStatLevel
    {
        public StatType statType;
        public float rawValue;
        public float percentValue;
    }
    
    [Serializable]
    public class DamageBuffEffectLevel
    {
        public List<DamageBuffStatLevel> stats;
        public float duration;
    }
    
    [CreateAssetMenu(fileName = "DamageBuffEffectConfig", menuName = "Unit/Effects/DamageBuffEffectConfig")]
    public class DamageBuffEffectConfig : EffectConfig
    {
        public List<DamageBuffEffectLevel> _levels;
        
        public override Effect CreateInstance()
        {
            return new DamageBuffEffect(this);
        }
    }
}
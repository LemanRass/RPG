using Effects.Configs;
using Effects.Core;
using Enums;
using UnityEngine;

namespace Effects
{
    public class HealthBuffEffect : Effect
    {
        public HealthBuffEffect(HealthBuffEffectConfig config)
        {
            statType = StatType.MAX_HEALTH;
            duration = config.duration;
            rawValue = config.rawValue;
            percentValue = config.percentValue;
        }

        public override string ToString()
        {
            return $"[HealthBuff] Max health +{Mathf.RoundToInt(percentValue * 100)}% for {duration} seconds.";
        }
    }
}
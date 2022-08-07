using System;
using System.Collections.Generic;
using Effects.Core;
using Enums;
using UnityEngine;

namespace Effects
{
    [Serializable]
    public class HealthBuffEffectLevel
    {
        public float rawValue;
        public float percentValue;
        public float duration;
    }
    
    [CreateAssetMenu(fileName = "HealthBuffEffect", menuName = "Unit/Effects/HealthBuffEffect")]
    public class HealthBuffEffect : Effect
    {
        public StatType statType;
        public List<HealthBuffEffectLevel> levels;
        public int level { get; private set; }
        private Unit _owner;

        public float durationTicks { get; private set; }

        public override void Execute(Unit owner, int level)
        {
            _owner = owner;
            this.level = level;
        }

        public override void Update()
        {
            if (isFinished) return;
            
            durationTicks += Time.deltaTime;

            if (durationTicks > levels[level].duration)
            {
                durationTicks = levels[level].duration;
                isFinished = true;
            }
        }

        public override float CalculateRawValue(StatType type)
        {
            return statType == type ? levels[level].rawValue : 0.0f;
        }

        public override float CalculatePercentageValue(StatType type)
        {
            return statType == type ? levels[level].percentValue : 0.0f;
        }

        public override string ToString()
        {
            return $"[HealthBuff] Max health +{Mathf.RoundToInt(levels[level].percentValue * 100)}% for {levels[level].duration} seconds.";
        }
    }
}
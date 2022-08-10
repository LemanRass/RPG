using System;
using System.Collections.Generic;
using Effects.Core;
using Enums;
using UnityEngine;

namespace Effects
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
    
    [CreateAssetMenu(fileName = "DamageBuffEffect", menuName = "Unit/Effects/DamageBuffEffect")]
    public class DamageBuffEffect : Effect
    {
        [SerializeField] private List<DamageBuffEffectLevel> _levels;

        private Unit _owner;
        private DamageBuffEffectLevel _level;

        private float _durationTicks;
        
        public override void Execute(Unit owner, int level)
        {
            _owner = owner;
            _level = _levels[level];
            _durationTicks = 0.0f;            
            isFinished = false;
        }

        public override void Update()
        {
            if (isFinished) return;

            _durationTicks += Time.deltaTime;

            if (_durationTicks > _level.duration)
            {
                _durationTicks = _level.duration;
                isFinished = true;
            }
        }

        public override float CalculatePercentageValue(StatType statType)
        {
            for (int i = 0; i < _level.stats.Count; i++)
            {
                var stat = _level.stats[i];

                if (stat.statType == statType)
                {
                    return stat.percentValue;
                }
            }

            return base.CalculatePercentageValue(statType);
        }

        public override float CalculateRawValue(StatType statType)
        {
            for (int i = 0; i < _level.stats.Count; i++)
            {
                var stat = _level.stats[i];

                if (stat.statType == statType)
                {
                    return stat.rawValue;
                }
            }

            return base.CalculateRawValue(statType);
        }
    }
}
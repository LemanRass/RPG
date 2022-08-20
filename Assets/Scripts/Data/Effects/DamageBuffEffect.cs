using Configs.Effects;
using Enums;
using UnityEngine;

namespace Data.Effects
{
    public class DamageBuffEffect : Effect
    {
        public new readonly DamageBuffEffectConfig config;
        public DamageBuffEffect(DamageBuffEffectConfig config) : base(config)
        {
            this.config = config;
        }

        private Unit _owner;
        private DamageBuffEffectLevel _level;
        private float _durationTicks;
        
        public override void Execute(Unit owner, int level)
        {
            _owner = owner;
            _level = config._levels[level - 1];
            _durationTicks = 0.0f;            
            isFinished = false;
        }

        public override void Dispose()
        {
            isFinished = true;
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
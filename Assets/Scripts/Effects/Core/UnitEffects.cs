using System.Collections.Generic;
using Configs;
using Enums;

namespace Effects.Core
{
    public class UnitEffects
    {
        private Unit _owner;
        public readonly Dictionary<EffectType, Effect> effects;
        
        public UnitEffects(Unit owner)
        {
            _owner = owner;
            effects = new Dictionary<EffectType, Effect>();
        }

        public void Update()
        {
            foreach (var effect in effects.Values)
            {
                effect.Update();

                if (effect.isFinished)
                {
                    RemoveEffect(effect);
                }
            }
        }
        
        public void AddEffect(EffectType effectType, int level)
        {
            if (effects.ContainsKey(effectType))
                return;

            var effect = ConfigsManager.effects[effectType];
            effect.Execute(_owner, level);
            effects.Add(effectType, effect);
        }

        public void RemoveEffect(Effect effect)
        {
            effects.Remove(effect.type);
        }

        public void ApplyEffects(StatType type, ref float basicValue)
        {
            float raw = 0.0f;
            float percentage = 0.0f;

            foreach (var effect in effects.Values)
            {
                raw += effect.CalculateRawValue(type);
                percentage += effect.CalculatePercentageValue(type);
            }

            basicValue += raw;
            basicValue *= (1.0f + percentage);
        }
    }
}
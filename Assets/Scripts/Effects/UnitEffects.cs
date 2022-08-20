using System.Collections.Generic;
using Data.Effects;
using Enums;
using UnityEngine;

namespace Effects
{
    public class UnitEffects
    {
        private readonly Unit _owner;
        private readonly Dictionary<EffectType, Effect> _effects;
        private readonly List<EffectType> _effectKeys;
        public Effect this[EffectType effectType] => _effects[effectType];
        public Effect this[int index] => _effects[_effectKeys[index]];
        public int count => _effects.Count;
        
        public UnitEffects(Unit owner)
        {
            _owner = owner;
            _effectKeys = new List<EffectType>();
            _effects = new Dictionary<EffectType, Effect>();
        }

        public void Update()
        {
            for (int i = 0; i < _effectKeys.Count; i++)
            {
                var key = _effectKeys[i];
                var effect = _effects[key];
                
                effect.Update();

                if (effect.isFinished)
                {
                    RemoveEffect(effect);
                    i--;
                }
            }
        }
        
        public void AddEffect(EffectType effectType, int level)
        {
            if (_effects.ContainsKey(effectType))
                return;

            var effect = Factory.Create(effectType);
            effect.Execute(_owner, level);
            _effects.Add(effectType, effect);
            _effectKeys.Add(effectType);
            
            Debug.Log($"Added effect {effectType} on level {level}.");
        }

        public void RemoveEffect(EffectType effectType)
        {
            if (_effects.ContainsKey(effectType))
            {
                _effects[effectType].Dispose();
            }
        }

        public bool ContainsEffect(EffectType effectType) => _effects.ContainsKey(effectType);

        public void RemoveEffect(Effect effect)
        {
            _effects.Remove(effect.config.type);
            _effectKeys.Remove(effect.config.type);
            
            Debug.Log($"Removed effect {effect.config.type}.");
        }

        public void ApplyEffects(StatType type, ref float basicValue)
        {
            float raw = 0.0f;
            float percentage = 0.0f;

            foreach (var effect in _effects.Values)
            {
                raw += effect.CalculateRawValue(type);
                percentage += effect.CalculatePercentageValue(type);
            }

            basicValue += raw;
            basicValue *= (1.0f + percentage);
        }
    }
}
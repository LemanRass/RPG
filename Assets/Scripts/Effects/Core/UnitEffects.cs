using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Effects.Core
{
    public class UnitEffects
    {
        private readonly List<Effect> _effects;
        
        public UnitEffects()
        {
            _effects = new List<Effect>();
        }

        public void Update()
        {
            for (int i = 0; i < _effects.Count; i++)
            {
                _effects[i].Update();
                _effects[i].duration -= Time.deltaTime;

                if (_effects[i].duration < 0.0f)
                {
                    RemoveEffect(_effects[i]);
                    i--;
                }
            }
        }
        
        public void AddEffect(Effect effect)
        {
            if (_effects.Contains(effect))
                return;

            _effects.Add(effect);
            Debug.Log(effect.ToString());
        }

        public void RemoveEffect(Effect effect)
        {
            _effects.Remove(effect);
            Debug.Log("Removed: " + effect);
        }

        public void ApplyEffects(StatType type, ref float basicValue)
        {
            float raw = 0.0f;
            float percentage = 0.0f;
            
            for (int i = 0; i < _effects.Count; i++)
            {
                if (_effects[i].statType == type)
                {
                    raw += _effects[i].rawValue;
                    percentage += _effects[i].percentValue;
                }
            }

            basicValue += raw;
            basicValue *= (1.0f + percentage);
        }
    }
}
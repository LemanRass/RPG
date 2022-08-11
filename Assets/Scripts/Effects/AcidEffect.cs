using System;
using System.Collections.Generic;
using Effects.Core;
using UnityEngine;

namespace Effects
{
    [Serializable]
    public class AcidEffectLevel
    {
        public float duration;
        public float interval;
        public float damage;
    }
    
    [CreateAssetMenu(fileName = "AcidEffect", menuName = "Unit/Effects/AcidEffect")]
    public class AcidEffect : Effect
    {
        public List<AcidEffectLevel> levels;
        
        public int level { get; private set; }
        private Unit _owner;
        
        private float _intervalTicks;
        public float durationTicks { get; private set; }

        public override void Execute(Unit owner, int level)
        {
            _owner = owner;
            this.level = level;
            
            _intervalTicks = 0.0f;
            durationTicks = 0.0f;

            isFinished = false;
        }

        public override void Dispose()
        {
            isFinished = true;
        }

        public override void Update()
        {
            if (isFinished) return;
            
            _intervalTicks += Time.deltaTime;
            durationTicks += Time.deltaTime;
            
            if (_intervalTicks >= levels[level].interval)
            {
                _owner.AddDamage(levels[level].damage);
                Debug.Log($"[AcidEffect] Add damage: {levels[level].damage}.");
                _intervalTicks = 0.0f;
            }

            if (durationTicks > levels[level].duration)
            {
                durationTicks = levels[level].duration;
                isFinished = true;
            }
        }
    }
}
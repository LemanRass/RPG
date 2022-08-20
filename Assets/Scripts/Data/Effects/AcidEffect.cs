using Configs.Effects;
using UnityEngine;

namespace Data.Effects
{
    public class AcidEffect : Effect
    {
        public new readonly AcidEffectConfig config;
        public AcidEffect(AcidEffectConfig config) : base(config)
        {
            this.config = config;
        }
        
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
            
            if (_intervalTicks >= config.levels[level].interval)
            {
                _owner.AddDamage(config.levels[level].damage);
                Debug.Log($"[AcidEffect] Add damage: {config.levels[level].damage}.");
                _intervalTicks = 0.0f;
            }

            if (durationTicks > config.levels[level].duration)
            {
                durationTicks = config.levels[level].duration;
                isFinished = true;
            }
        }
    }
}
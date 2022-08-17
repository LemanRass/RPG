using Configs.Effects;
using Enums;

namespace Effects.Core
{
    public class Effect
    {
        public readonly EffectConfig config;
        public Effect(EffectConfig config)
        {
            this.config = config;
        }
        
        public bool isFinished { get; protected set; }

        public virtual void Execute(Unit owner, int level) {}
        public virtual void Dispose() {}
        public virtual void Update() { }
        public virtual float CalculateRawValue(StatType statType) => 0.0f;
        public virtual float CalculatePercentageValue(StatType statType) => 0.0f;
    }
}
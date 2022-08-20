using Configs.Effects;
using Enums;

namespace Data.Effects
{
    public abstract class Effect
    {
        public readonly EffectConfig config;
        public Effect(EffectConfig config)
        {
            this.config = config;
        }
        
        public bool isFinished { get; protected set; }

        public abstract void Execute(Unit owner, int level);
        public virtual void Dispose() {}
        public virtual void Update() { }
        public virtual float CalculateRawValue(StatType statType) => 0.0f;
        public virtual float CalculatePercentageValue(StatType statType) => 0.0f;
    }
}
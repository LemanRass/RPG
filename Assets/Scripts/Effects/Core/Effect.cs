using Enums;
using UnityEngine;

namespace Effects.Core
{
    public abstract class Effect : ScriptableObject
    {
        public string name;
        public Sprite icon;
        public EffectType type;
        public bool isFinished { get; protected set; }

        public abstract void Execute(Unit owner, int level);
        public abstract void Dispose();
        public virtual void Update() { }
        public virtual float CalculateRawValue(StatType statType) => 0.0f;
        public virtual float CalculatePercentageValue(StatType statType) => 0.0f;
    }
}
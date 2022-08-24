using Data.Effects;
using Enums;
using UnityEngine;

namespace Configs.Effects
{
    public abstract class EffectConfig : ScriptableObject
    {
        public EffectType type;
        public string name;
        public Sprite icon;
        public ParticleSystem initEffect;

        public abstract Effect CreateInstance();
    }
}
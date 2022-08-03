using Effects.Core;
using UnityEngine;

namespace Effects
{
    public class AcidEffect : Effect
    {
        public Unit target;
        public float interval;
        public float damage;
        private float _ticks;

        public AcidEffect(Unit target, float duration, float interval, float damage)
        {
            this.target = target;
            this.duration = duration;
            this.interval = interval;
            this.damage = damage;
        }

        public override void Update()
        {
            _ticks += Time.deltaTime;

            if (_ticks >= interval)
            {
                target.AddDamage(damage);
                _ticks = 0.0f;
                Debug.Log($"[{target.name}] has {target.health} health now.");
            }
        }
    }
}
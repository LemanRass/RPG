using System;
using System.Threading.Tasks;
using Data.Skills;
using UnityEngine;

namespace Skills
{
    public class SkillCaster
    {
        public event Action<Skill> onCastStarted;
        public event Action<float> onCastingProgress;
        public event Action<Skill> onCastCompleted;
        public event Action onCastCancelled;

        public Skill skill { get; private set; }

        public bool isCasting { get; private set; }
        public float castingTicks { get; private set; }
        public float castingProgress { get; private set; }

        private Unit _unit;
        private TaskCompletionSource<bool> _promise;

        public SkillCaster(Unit unit)
        {
            _unit = unit;
        }
        
        public async Task<bool> Cast(Skill skill)
        {
            if (isCasting)
                return false;

            _promise = new TaskCompletionSource<bool>();
            
            this.skill = skill;
            castingTicks = 0.0f;
            _isVfxCasted = false;
            isCasting = true;
            
            onCastStarted?.Invoke(skill);
            
            return await _promise.Task;
        }

        public void Cancel()
        {
            if (isCasting)
            {
                isCasting = false;
                castingTicks = 0.0f;
                skill = null;

                onCastCancelled?.Invoke();
                _promise.SetResult(false);
            }
        }

        public void Complete()
        {
            if (isCasting)
            {
                isCasting = false;
                castingTicks = 0.0f;
                onCastCompleted?.Invoke(skill);
                skill = null;
                _promise.SetResult(true);
            }
        }

        private bool _isVfxCasted;
        
        public void Update()
        {
            if (isCasting)
            {
                castingTicks += Time.deltaTime;
                castingProgress = castingTicks / skill.config.castingDuration;
                onCastingProgress?.Invoke(castingProgress);

                if (!_isVfxCasted)
                {
                    if (castingProgress >= skill.config.vfx.startAtProgress)
                    {
                        if (skill.config.vfx.particle != null)
                        {
                            var particle = GameObject.Instantiate(skill.config.vfx.particle, _unit.transform);
                            particle.Emit(1);
                        }
                        _isVfxCasted = true;
                    }
                }
                
                if (castingTicks >= skill.config.castingDuration)
                {
                    Complete();
                }
            }
        }
    }
}
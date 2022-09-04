using System;
using Data.Skills;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Views.PlayerStateMachine
{
    public class PlayerCastSkillState : PlayerState
    {
        private IDisposable _vfxObserver;
        private IDisposable _castObserver;
        
        private readonly Skill _skill;

        public PlayerCastSkillState(IPlayerStateMachine player, Skill skill)
            : base(player)
        {
            _skill = skill;
            player.unit.skills.skillCaster.onCastCancelled += OnCastCancelled;
        }
        
        private void OnCastCancelled(Skill skill)
        {
            _vfxObserver?.Dispose();
            _castObserver?.Dispose();
            
            _player.machine.ChangeState(new PlayerIdleState(_player));
        }

        public override void OnEnter()
        {
            var receiver = _player.unit.target.selected;
            var direction = Vector3.Normalize(receiver.transform.position - _player.transform.position);
            _player.transform.forward = direction;
            
            float duration = _skill.config.castingDuration;
            float animLength = _skill.config.anim.length;

            _player.animator.SetFloat("CastSpeed", animLength / duration);
            _player.animator.Play(_skill.config.anim.name);

            float vfxDuration = duration * _skill.config.vfx.startAtProgress;
            
            _vfxObserver = Observable
                .Timer(TimeSpan.FromSeconds(vfxDuration))
                .Subscribe(_ =>
                {
                    if (_skill.config.vfx.particle != null)
                    {
                        var anchor = _player.unit.skills.GetSkillAnchor(_skill.config.vfx.anchorType);
                        var particle = Object.Instantiate(_skill.config.vfx.particle, anchor.transform);
                        particle.transform.localPosition = Vector3.zero;
                        particle.Play();
                    }
                });
            
            _castObserver = Observable
                .Timer(TimeSpan.FromSeconds(duration))
                .Subscribe(_ =>
                {
                    _player.machine.ChangeState(new PlayerIdleState(_player));
                });
        }
        
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _player.unit.skills.skillCaster.Cancel();
            }
        }

        public override void OnExit()
        {
            _player.unit.skills.skillCaster.onCastCancelled -= OnCastCancelled;
        }
    }
}
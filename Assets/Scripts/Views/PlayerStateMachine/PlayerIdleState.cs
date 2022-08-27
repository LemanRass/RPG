using Data.Skills;
using UnityEngine;

namespace Views.PlayerStateMachine
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(IPlayerStateMachine player) 
            : base(player)
        {
        }

        public override void OnEnter()
        {
            _player.animator.Play("Idle");
            
            _player.unit.skills.skillCaster.onCastStarted += OnCastStarted;
        }

        private void OnCastStarted(Skill skill)
        {
            _player.machine.ChangeState(new PlayerCastSkillState(_player, skill));
        }

        public override void OnUpdate()
        {
            if (CheckForMove(out var point))
            {
                _player.machine.ChangeState(new PlayerMoveState(_player, point));
            }
        }

        public override void OnExit()
        {
            _player.unit.skills.skillCaster.onCastStarted -= OnCastStarted;
        }
    }
}
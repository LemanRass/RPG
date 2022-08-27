
using Data.Skills;
using UnityEngine;

namespace Views.PlayerStateMachine
{
    public class PlayerMoveState : PlayerState
    {
        private Vector3 _pointToMove;
        
        public PlayerMoveState(IPlayerStateMachine player, Vector3 pointToMove) 
            : base(player)
        {
            _pointToMove = pointToMove;
        }
        
        private void OnCastStarted(Skill skill)
        {
            _player.machine.ChangeState(new PlayerCastSkillState(_player, skill));
        }

        public override void OnEnter()
        {
            _player.animator.Play("RunForward");
            _player.unit.skills.skillCaster.onCastStarted += OnCastStarted;
        }

        public override void OnUpdate()
        {
            if (CheckForMove(out var point))
                _pointToMove = point;
            
            _player.transform.position = Vector3.MoveTowards(
                _player.transform.position, 
                _pointToMove,
                5.0f * Time.deltaTime);

            var distance = Vector3.Distance(_player.transform.position, _pointToMove);
            if (distance < 0.01f)
                _player.machine.ChangeState(new PlayerIdleState(_player));
        }

        public override void OnExit()
        {
            _player.unit.skills.skillCaster.onCastStarted -= OnCastStarted;
        }
    }
}
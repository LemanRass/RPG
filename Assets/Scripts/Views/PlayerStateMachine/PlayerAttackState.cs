using UnityEngine;

namespace Views.PlayerStateMachine
{
    public class PlayerAttackState : PlayerState
    {
        private readonly Unit _targetUnit;
        private const float ATTACK_DURATION = 2.0f;

        private float _attackTicks;

        private PlayerState _nextState;
        
        public PlayerAttackState(IPlayerStateMachine player, Unit target) : base(player)
        {
            _targetUnit = target;
            _attackTicks = float.MaxValue;
        }

        public override void OnEnter()
        {
            _player.enemyPointView.Show(_targetUnit);
        }

        public override void OnUpdate()
        {
            if (CheckForMove(out var point))
            {
                _nextState = new PlayerMoveState(_player, point);
            }
            
            _attackTicks += Time.deltaTime;

            if (_attackTicks >= ATTACK_DURATION)
            {
                if (_nextState != null)
                {
                    _player.machine.ChangeState(_nextState);
                    return;
                }
                
                var distance = Vector3.Distance(_player.transform.position, _targetUnit.transform.position);
                if (distance > 2.0f)
                {                
                    _player.machine.ChangeState(new PlayerChasingState(_player, _targetUnit));
                    return;
                }
                
                _player.animator.SetTrigger($"MeleeAttack_{Random.Range(0, 2) + 1}");
                _attackTicks = 0.0f;
            }
        }

        public override void OnExit()
        {
            _player.enemyPointView.Hide();
        }
    }
}
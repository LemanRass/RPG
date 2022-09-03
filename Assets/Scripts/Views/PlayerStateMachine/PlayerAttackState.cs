using Enums;
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
            
            _player.unit.talents.onTalentsChanged += RecalculateStats;
            _player.unit.effects.onEffectsChanged += RecalculateStats;
            _player.unit.equipment.onEquipmentChanged += RecalculateStats;
            
            RecalculateStats();
        }

        private void RecalculateStats()
        {
            _player.animator.SetFloat("MeleeAttackSpeed", _player.unit.GetStat(StatType.MELEE_ATTACK_SPEED));
            _player.animator.SetFloat("MoveSpeed", _player.unit.GetStat(StatType.MOVE_SPEED) / 5.0f);
        }
        
        public override void OnUpdate()
        {
            if (CheckForMove(out var point))
            {
                _nextState = new PlayerMoveState(_player, point);
            }
            
            _attackTicks += Time.deltaTime;

            if (_attackTicks >= 1.0f / _player.unit.GetStat(StatType.MELEE_ATTACK_SPEED))
            {
                if (_nextState != null)
                {
                    _player.machine.ChangeState(_nextState);
                    return;
                }
                
                var distance = Vector3.Distance(_player.transform.position, _targetUnit.transform.position);
                if (distance > _player.unit.GetStat(StatType.ATTACK_DISTANCE))
                {                
                    _player.machine.ChangeState(new PlayerChasingState(_player, _targetUnit));
                    return;
                }
                
                _player.animator.SetTrigger($"MeleeAttack_{Random.Range(1, 3).ToString()}");
                _attackTicks = 0.0f;
            }
        }

        public override void OnExit()
        {
            _player.unit.talents.onTalentsChanged -= RecalculateStats;
            _player.unit.effects.onEffectsChanged -= RecalculateStats;
            _player.unit.equipment.onEquipmentChanged -= RecalculateStats;
            
            _player.enemyPointView.Hide();
        }
    }
}
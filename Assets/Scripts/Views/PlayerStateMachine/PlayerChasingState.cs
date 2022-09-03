using Enums;
using UnityEngine;

namespace Views.PlayerStateMachine
{
    public class PlayerChasingState : PlayerState
    {
        private readonly Unit _targetUnit;
        
        public PlayerChasingState(IPlayerStateMachine player, Unit target) : base(player)
        {
            _targetUnit = target;
        }

        public override void OnEnter()
        {
            _player.enemyPointView.Show(_targetUnit);
            _player.animator.SetBool("IsRunning", true);
            
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
                _player.machine.ChangeState(new PlayerMoveState(_player, point));
                return;
            }
            
            _player.transform.position = Vector3.MoveTowards(
                _player.transform.position, 
                _targetUnit.transform.position,
                5.0f * Time.deltaTime);

            _player.transform.forward =
                Vector3.Normalize(_targetUnit.transform.position - _player.transform.position);

            var distance = Vector3.Distance(_player.transform.position, _targetUnit.transform.position);
            if (distance < _player.unit.GetStat(StatType.ATTACK_DISTANCE))
            {
                _player.machine.ChangeState(new PlayerAttackState(_player, _targetUnit));
            }
        }

        public override void OnExit()
        {
            _player.unit.talents.onTalentsChanged -= RecalculateStats;
            _player.unit.effects.onEffectsChanged -= RecalculateStats;
            _player.unit.equipment.onEquipmentChanged -= RecalculateStats;
            
            _player.enemyPointView.Hide();
            _player.animator.SetBool("IsRunning", false);
        }
    }
}

using Data.Items;
using Data.Skills;
using Enums;
using Equipment;
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
            _player.animator.SetBool("IsRunning", true);
            _player.unit.skills.skillCaster.onCastStarted += OnCastStarted;
            _player.unit.equipment[EquipmentType.RIGHT_HAND].onChanged += OnWeaponChanged;
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

        private void OnWeaponChanged(EquipmentItemData itemData)
        {
            _player.animator.SetInteger("WeaponType", itemData == null ? 0 : 1);
            _player.animator.SetTrigger("WeaponEquip");
        }
        
        public override void OnUpdate()
        {
            if (CheckForChase(out var target))
            {
                _player.machine.ChangeState(new PlayerChasingState(_player, target));
                return;
            }
            
            if (CheckForMove(out var point))
                _pointToMove = point;
            
            _player.transform.position = Vector3.MoveTowards(
                _player.transform.position, 
                _pointToMove,
                _player.unit.GetStat(StatType.MOVE_SPEED) * Time.deltaTime);
            
            _player.transform.forward = Vector3.Normalize(_pointToMove - _player.transform.position);

            var distance = Vector3.Distance(_player.transform.position, _pointToMove);
            if (distance < 0.5f)
            {
                _player.machine.ChangeState(new PlayerIdleState(_player));
            }
        }

        public override void OnExit()
        {
            _player.animator.SetBool("IsRunning", false);
            _player.movePointView.Hide();
            _player.unit.skills.skillCaster.onCastStarted -= OnCastStarted;
            _player.unit.equipment[EquipmentType.RIGHT_HAND].onChanged -= OnWeaponChanged;
            _player.unit.talents.onTalentsChanged -= RecalculateStats;
            _player.unit.effects.onEffectsChanged -= RecalculateStats;
            _player.unit.equipment.onEquipmentChanged -= RecalculateStats;
        }
    }
}
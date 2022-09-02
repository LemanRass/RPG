
using Data.Items;
using Data.Skills;
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
                5.0f * Time.deltaTime);
            
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
        }
    }
}
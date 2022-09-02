using Data.Items;
using Data.Skills;
using Equipment;

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
            _player.unit.skills.skillCaster.onCastStarted += OnCastStarted;
            _player.unit.equipment[EquipmentType.RIGHT_HAND].onChanged += OnWeaponChanged;
        }

        private void OnWeaponChanged(EquipmentItemData itemData)
        {
            _player.animator.SetInteger("WeaponType", itemData == null ? 0 : 1);
            _player.animator.SetTrigger("WeaponEquip");
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

            if (CheckForChase(out var target))
            {
                _player.machine.ChangeState(new PlayerChasingState(_player, target));
            }
        }

        public override void OnExit()
        {
            _player.unit.skills.skillCaster.onCastStarted -= OnCastStarted;
            _player.unit.equipment[EquipmentType.RIGHT_HAND].onChanged -= OnWeaponChanged;
        }
    }
}
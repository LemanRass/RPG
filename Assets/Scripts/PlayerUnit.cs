using Enums;
using UnityEngine;

public class PlayerUnit : Unit
{
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipItem(inventory[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipItem(inventory[1]);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            talents[TalentType.STRENGTH].level++;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            UseSkill(SkillType.DAMAGE_BUFF, this);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log($"[Damage]: {GetStat(StatType.MELEE_MIN_DAMAGE)}/{GetStat(StatType.MELEE_MAX_DAMAGE)}");
        }
        
        base.Update();
    }
}
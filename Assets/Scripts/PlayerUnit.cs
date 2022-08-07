using System.Collections;
using Enums;
using Inventory;
using UnityEngine;

public class PlayerUnit : Unit
{
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var currHealth = GetStat(StatType.MAX_HEALTH);
            Debug.Log($"Current health: {currHealth}");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            talents.talents[TalentType.VITALITY].level++;
            Debug.Log($"Vitality is {talents.talents[TalentType.VITALITY]} level.");
        }
        
        base.Update();
    }

    private IEnumerator EquipItem(Item item)
    {
        Debug.Log("Start equipping...");
        yield return new WaitForSeconds(1.0f);
        equipment.Equip(item);
        Debug.Log("Equipped!");
    }
}
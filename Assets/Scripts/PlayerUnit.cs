using System.Collections.Generic;
using Configs.Items;
using UnityEngine;

public class PlayerUnit : Unit
{
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipItem(inventory.slots[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipItem(inventory.slots[1]);
        }
        
        base.Update();
    }
}
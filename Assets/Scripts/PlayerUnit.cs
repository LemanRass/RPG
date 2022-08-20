using Enums;
using UnityEngine;

public class PlayerUnit : Unit
{
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log($"MaxHealth: {GetStat(StatType.MAX_HEALTH)}");
        }

        base.Update();
    }
}
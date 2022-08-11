using System.Collections.Generic;
using BasicStats;
using BasicStats.Data;
using Configs.Items;
using Effects.Core;
using Enums;
using Equipment;
using Inventory;
using Inventory.Data;
using Skills.Core;
using Talents;
using Talents.Data;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected UnitStatsData _statsData;
    [SerializeField] protected UnitTalentsData _talentsData;
    [SerializeField] protected UnitInventoryData _inventoryData; 

    public float health { get; private set; }
    
    public UnitBasicStats basicStats;
    public UnitEffects effects;
    public UnitTalents talents;
    public UnitSkills skills;
    public UnitEquipment equipment;
    public UnitInventory inventory;

    protected virtual void Start()
    {
        basicStats = new UnitBasicStats(_statsData);
        effects = new UnitEffects(this);
        talents = new UnitTalents(_talentsData);
        skills = new UnitSkills();
        equipment = new UnitEquipment(this);
        inventory = new UnitInventory(_inventoryData);
    }

    protected virtual void Update()
    {
        effects.Update();
        skills.Update();
    }

    public float GetStat(StatType type)
    {
        float value = basicStats[type].value;
        talents.ApplyTalents(type, ref value);
        equipment.ApplyEquipment(type, ref value);
        effects.ApplyEffects(type, ref value);
        value = Mathf.Clamp(value, 0, float.MaxValue);
        return value;
    }

    public void AddDamage(float damage)
    {
        health -= damage;
    }

    public void UseSkill(SkillType skillType, Unit receiver)
    {
        if (skills[skillType].CheckIfCanUseSkill(this, receiver))
        {
            skills.UseSkill(skillType, this, receiver);
        }
    }

    public void AddEffect(EffectType effectType, int level)
    {
        effects.AddEffect(effectType, level);
    }

    public void UseInventorySlot(InventorySlot inventorySlot)
    {
        if (inventorySlot.isEmpty)
            return;

        var item = inventorySlot.item;
        inventorySlot.Clear();
        
        var equipmentSlot = equipment[item.equipmentType];

        if (!equipmentSlot.isEmpty)
        {
            UseEquipmentSlot(equipmentSlot);
        }
        
        equipmentSlot.Insert(item);
    }

    public void UseEquipmentSlot(EquipmentSlot equipmentSlot)
    {
        if (equipmentSlot.isEmpty)
            return;
        
        var item = equipmentSlot.item;
        equipment.DeEquip(equipmentSlot);
        var inventorySlot = inventory.FindFreeSlot();
        inventorySlot.Insert(item);
    }
}

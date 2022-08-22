using System;
using BasicStats;
using BasicStats.Data;
using Data.Items;
using Effects;
using Enums;
using Equipment;
using Inventory;
using Inventory.Data;
using Skills;
using Talents;
using Talents.Data;
using UnityEngine;


public class Unit : MonoBehaviour
{
    [SerializeField] protected UnitStatsData _statsData;
    [SerializeField] protected UnitTalentsData _talentsData;
    [SerializeField] protected UnitInventoryData _inventoryData;

    public int health;
    public int mana;

    public event Action<int> onHealthChanged;
    public event Action<int> onManaChanged;


    public UnitBasicStats basicStats { get; private set; }
    public UnitEffects effects { get; private set; }
    public UnitTalents talents { get; private set; }
    public UnitSkills skills { get; private set; }
    public UnitEquipment equipment { get; private set; }
    public UnitInventory inventory { get; private set; }

    protected virtual void Awake()
    {
        basicStats = new UnitBasicStats(_statsData);
        effects = new UnitEffects(this);
        talents = new UnitTalents(_talentsData);
        skills = new UnitSkills();
        equipment = new UnitEquipment(this);
        inventory = new UnitInventory(_inventoryData);
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        skills.Update();
        effects.Update();
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

    public void AddDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, int.MaxValue);
        onHealthChanged?.Invoke(health);
    }

    public void UseSkill(SkillType skillType, Unit receiver)
    {
        skills.UseSkill(skillType, this, receiver);
    }

    public void AddEffect(EffectType effectType, int level)
    {
        effects.AddEffect(effectType, level);
    }

    private void EquipItem(EquipmentItemData item)
    {
        var equipmentSlot = equipment[item.config.equipmentType];

        if (!equipmentSlot.isEmpty)
        {
            UseEquipmentSlot(equipmentSlot);
        }
        
        equipmentSlot.Insert(item);
    }

    public void UseInventorySlot(InventorySlot inventorySlot)
    {
        if (inventorySlot.isEmpty)
            return;

        switch (inventorySlot.item)
        {
            case EquipmentItemData equipmentItem:
                inventorySlot.Clear();
                EquipItem(equipmentItem);
                break;
            
            case RemedyItemData remedyItem:
                remedyItem.Use(this);
                if (remedyItem.count < 1)
                    inventorySlot.Clear();
                break;
        }
    }
    
    public void UseEquipmentSlot(EquipmentSlot equipmentSlot)
    {
        if (equipmentSlot.isEmpty)
            return;
        
        var item = equipmentSlot.equipmentItem;
        equipment.DeEquip(equipmentSlot);
        var inventorySlot = inventory.FindFreeSlot();
        inventorySlot.Insert(item);
    }

    public void DropInventorySlotIntoInventorySlot(InventorySlot from, InventorySlot to)
    {
        inventory.Swap(from, to);
    }

    public void DropInventorySlotIntoEquipmentSlot(InventorySlot from, EquipmentSlot to)
    {
        equipment.Swap(from, to);
    }

    public void DropEquipmentSlotIntoInventorySlot(EquipmentSlot from, InventorySlot to)
    {
        inventory.Swap(from, to);
    }

    public void SplitInventorySlot(InventorySlot from, InventorySlot to, int count)
    {
        inventory.Split(from, to, count);
    }
}

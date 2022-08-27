using Data.Items;
using Enums;
using Equipment;
using Inventory;
using Inventory.Data;
using Skills;
using Target;
using UnityEngine;

public class PlayerUnit : Unit
{
    [SerializeField] protected UnitInventoryData _inventoryData;

    public UnitSkills skills { get; private set; }
    public UnitEquipment equipment { get; private set; }
    public UnitInventory inventory { get; private set; }
    public UnitTarget target { get; private set; }
    
    public bool isInitialized { get; private set; }
    
    
    protected override void Start()
    {
        skills = new UnitSkills(this);
        equipment = new UnitEquipment(this);
        inventory = new UnitInventory(_inventoryData);
        target = new UnitTarget();
        base.Start();
        
        isInitialized = true;
    }

    protected override void Update()
    {
        skills.Update();
        base.Update();
    }
    
    public override float GetStat(StatType type)
    {
        float value = basicStats[type].value;
        talents.ApplyTalents(type, ref value);
        equipment.ApplyEquipment(type, ref value);
        effects.ApplyEffects(type, ref value);
        value = Mathf.Clamp(value, 0, float.MaxValue);
        return value;
    }
    
    public void UseSkill(SkillType skillType, Unit receiver)
    {
        skills.UseSkill(skillType, this, receiver);
    }
    
    public void UseInventorySlot(InventorySlot inventorySlot)
    {
        if (inventorySlot.isEmpty)
            return;

        switch (inventorySlot.item)
        {
            case EquipmentItemData equipmentItem:
                var equipmentSlot = equipment[equipmentItem.config.equipmentType];
                DropInventorySlotIntoEquipmentSlot(inventorySlot, equipmentSlot);
                break;
            
            case RemedyItemData remedyItem:
                remedyItem.Use(this);
                if (remedyItem.count < 1)
                {
                    inventory.ClearSlot(inventorySlot);
                }
                break;
        }
    }
    
    public void UseEquipmentSlot(EquipmentSlot equipmentSlot)
    {
        if (equipmentSlot.isEmpty)
            return;
        
        var inventorySlot = inventory.FindFreeSlot();
        if (inventorySlot == null)
            return;
        
        DropEquipmentSlotIntoInventorySlot(equipmentSlot, inventorySlot);
    }

    public void DropInventorySlotIntoInventorySlot(InventorySlot from, InventorySlot to)
    {
        inventory.Move(from, to);
    }

    public void DropInventorySlotIntoEquipmentSlot(InventorySlot from, EquipmentSlot to)
    {
        if (from.isEmpty)
            return;
        
        if (from.item is EquipmentItemData fromEquipment)
        {
            if (fromEquipment.config.equipmentType != to.equipmentType)
                return;

            inventory.InsertItem(from, to.equipmentItem);
            equipment.EquipItem(fromEquipment);
        }
    }

    public void DropEquipmentSlotIntoInventorySlot(EquipmentSlot from, InventorySlot to)
    {
        if (from.isEmpty)
            return;

        if (to.isEmpty)
        {
            inventory.InsertItem(to, from.equipmentItem);
            equipment.ClearSlot(from);
            return;
        }
        
        if (to.item is EquipmentItemData toEquipmentItem)
        {
            var fromEquipmentType = from.equipmentItem.config.equipmentType;
            var toEquipmentType = toEquipmentItem.config.equipmentType;
                
            if (fromEquipmentType != toEquipmentType)
                return;
                
            inventory.InsertItem(to, from.equipmentItem);
            equipment.EquipItem(toEquipmentItem);
        }
    }

    public void SplitInventorySlot(InventorySlot from, InventorySlot to, int count)
    {
        inventory.Split(from, to, count);
    }
}
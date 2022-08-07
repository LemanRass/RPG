using BasicStats;
using BasicStats.Data;
using Effects.Core;
using Enums;
using Equipment;
using Inventory;
using Skills.Core;
using Talents;
using Talents.Data;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected UnitStatsData _statsData;
    [SerializeField] protected UnitTalentsData _talentsData;

    public float health;
    
    public UnitBasicStats basicStats;
    public UnitEffects effects;
    public UnitTalents talents;
    public UnitSkills skills;
    public UnitEquipment equipment;
    public UnitInventory inventory;

    private void Start()
    {
        basicStats = new UnitBasicStats(_statsData);
        effects = new UnitEffects(this);
        talents = new UnitTalents(_talentsData);
        skills = new UnitSkills();
    }

    protected virtual void Update()
    {
        effects.Update();
        skills.Update();
    }

    public float GetStat(StatType type)
    {
        float value = basicStats[type];
        talents.ApplyTalents(type, ref value);
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
        //skills.UseSkill(skillType, receiver);
    }
}

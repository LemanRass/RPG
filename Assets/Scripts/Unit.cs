using System;
using BasicStats;
using BasicStats.Data;
using Effects;
using Enums;
using Talents;
using Talents.Data;
using UnityEngine;


public class Unit : MonoBehaviour
{
    [SerializeField] protected UnitStatsData _statsData;
    [SerializeField] protected UnitTalentsData _talentsData;

    public string name;
    public int health;
    public int mana;

    public event Action<int> onHealthChanged;
    public event Action<int> onManaChanged;


    public UnitBasicStats basicStats { get; private set; }
    public UnitEffects effects { get; private set; }
    public UnitTalents talents { get; private set; }
    
    protected virtual void Awake()
    {
        basicStats = new UnitBasicStats(_statsData);
        effects = new UnitEffects(this);
        talents = new UnitTalents(_talentsData);
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        effects.Update();
    }

    public virtual float GetStat(StatType type)
    {
        float value = basicStats[type].value;
        talents.ApplyTalents(type, ref value);
        effects.ApplyEffects(type, ref value);
        value = Mathf.Clamp(value, 0, float.MaxValue);
        return value;
    }

    public void AddDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, int.MaxValue);
        onHealthChanged?.Invoke(health);
    }

    public void AddEffect(EffectType effectType, int level)
    {
        effects.AddEffect(effectType, level);
    }
}

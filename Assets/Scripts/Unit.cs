using System.Collections.Generic;
using BasicStats;
using BasicStats.Data;
using Effects.Configs;
using Effects.Core;
using Enums;
using Skills.Configs;
using Talents;
using Talents.Data;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected List<SkillConfig> _skillConfigs;
    [SerializeField] protected List<EffectConfig> _effectsConfigs;
    
    [SerializeField] protected UnitStatsData _statsData;
    [SerializeField] protected UnitTalentsData _talentsData;

    public float health;
    
    protected UnitBasicStats _basicStats;
    protected UnitEffects _effects;
    protected UnitTalents _talents;

    private void Start()
    {
        _basicStats = new UnitBasicStats(_statsData);
        _effects = new UnitEffects();
        _talents = new UnitTalents(_talentsData);
    }

    protected virtual void Update()
    {
        _effects.Update();
    }

    public float GetStat(StatType type)
    {
        float value = _basicStats[type];
        _talents.ApplyTalents(type, ref value);
        _effects.ApplyEffects(type, ref value);
        value = Mathf.Clamp(value, 0, float.MaxValue);
        return value;
    }

    public void AddDamage(float damage)
    {
        health -= damage;
    }

    public void AddEffect(Effect effect)
    {
        _effects.AddEffect(effect);
    }
}

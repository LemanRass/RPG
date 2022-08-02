using System.Collections.Generic;
using Effects;
using Effects.Configs;
using Effects.Core;
using Enums;
using Talents;
using Talents.Configs;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private List<EffectConfig> _effectsConfigs;
    [SerializeField] private UnitTalentsConfig _talentsConfig;

    private UnitEffects _effects;
    private UnitTalents _talents;

    private void Start()
    {
        _effects = new UnitEffects();
        _talents = new UnitTalents(_talentsConfig);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _talents.LevelUp(TalentType.VITALITY);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _effects.AddEffect(new HealthBuffEffect((HealthBuffEffectConfig)_effectsConfigs[0]));
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log(GetStat(StatType.MAX_HEALTH));
        }
        
        _effects.Update();
    }

    public float GetStat(StatType type)
    {
        float rawValue = _talents.GetStat(type);
        return _effects.ApplyEffects(type, rawValue);
    }
}

using Effects;
using Effects.Configs;
using Enums;
using Skills;
using UnityEngine;

public class PlayerUnit : Unit
{
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _talents.AddExperience(TalentType.VITALITY, 5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _effects.AddEffect(new HealthBuffEffect((HealthBuffEffectConfig)_effectsConfigs[0]));
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log(GetStat(StatType.MAX_HEALTH));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //var skill = new LightningSkill();
            //skill.Execute(this, this);
            //new ExplosionSkill(this, this);
            new AcidSplashSkill(this, this, _skillConfigs[0]);
        }
            
        base.Update();
    }
}
using Cysharp.Threading.Tasks;
using Data.Skills;
using Enums;
using UnityEngine;

public class PlayerUnit : Unit
{
    [SerializeField] private Animator _animator;

    protected override void Start()
    {
        base.Start();
        
        skills.skillCaster.onCastStarted += OnCastStarted;
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            talents.AddExperience(TalentType.VITALITY, 35);
        }
    }

    private void OnCastStarted(Skill skill)
    {
        float duration = skill.config.castingDuration;
        float animLength = skill.config.anim.length;
        
        _animator.SetFloat("CastSpeed", animLength / duration);
        _animator.SetTrigger(skill.config.anim.name);
        
    }
}
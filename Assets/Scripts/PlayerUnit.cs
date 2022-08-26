using System.Collections;
using Data.Skills;
using Enums;
using UnityEngine;

public class PlayerUnit : Unit
{
    [SerializeField] private Animator _animator;

    private Coroutine _castSkillCoroutine;
    
    protected override void Start()
    {
        base.Start();
        
        skills.skillCaster.onCastStarted += OnCastStarted;
        skills.skillCaster.onCastCancelled += OnCastCancelled;
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            talents.AddExperience(TalentType.VITALITY, 35);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skills.skillCaster.Cancel();
        }
    }

    private void OnCastStarted(Skill skill)
    {
        _castSkillCoroutine = StartCoroutine(CastSkill(skill));
    }
    
    private void OnCastCancelled(Skill skill)
    {
        StopCoroutine(_castSkillCoroutine);
        _castSkillCoroutine = null;

        _animator.SetTrigger("CastCancel");
    }

    private IEnumerator CastSkill(Skill skill)
    {
        float duration = skill.config.castingDuration;
        float animLength = skill.config.anim.length;
        
        _animator.SetFloat("CastSpeed", animLength / duration);
        _animator.SetTrigger(skill.config.anim.name);

        float vfxStartTime = duration * skill.config.vfx.startAtProgress;
        yield return new WaitForSeconds(vfxStartTime);

        if (skill.config.vfx.particle != null)
        {
            var anchor = skills.GetSkillAnchor(skill.config.vfx.anchorType);
            var particle = Instantiate(skill.config.vfx.particle);
            particle.transform.position = anchor.transform.position;
            particle.transform.forward = Vector3.Normalize(Camera.main.transform.position - anchor.transform.position);
            particle.Emit(1);
        }
    }
}
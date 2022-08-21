using Data.Skills;
using UnityEngine;

public class PlayerUnit : Unit
{
    [SerializeField] private Animator _animator;

    protected override void Start()
    {
        base.Start();
        
        skills.skillCaster.onCastStarted += OnCastStarted;
    }

    private void OnCastStarted(Skill skill)
    {
        _animator.SetTrigger(skill.config.anim);
    }
}
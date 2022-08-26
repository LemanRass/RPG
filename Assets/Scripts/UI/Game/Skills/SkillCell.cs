using Data.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.Skills
{
    public class SkillCell : MonoBehaviour
    {
        [SerializeField] private Image _cooldownImg;
        [SerializeField] private Image _skillImg;
        [SerializeField] private Button _skillBtn;
        
        private Skill _skill;
        
        public void Init(Skill skill)
        {
            _skill = skill;
            _skill.cooldown.onStarted += OnStarted;
            _skill.cooldown.onProgress += OnProgress;
            _skill.cooldown.onFinished += OnFinished;

            var skillCaster = GameManager.instance.playerUnit.skills.skillCaster;
            skillCaster.onCastStarted += OnCastStarted;
            skillCaster.onCastCancelled += OnCastCancelled;
            skillCaster.onCastCompleted += OnCastCompleted;
            
            _skillImg.sprite = _skill.config.icon;
            _skillImg.gameObject.SetActive(_skill != null);
            _skillBtn.onClick.AddListener(OnSkillClick);
        }

        private void OnCastCompleted(Skill skill)
        {
            _skillBtn.interactable = true;
            if (!_skill.cooldown.isCoolingDown)
            {
                _cooldownImg.gameObject.SetActive(false);
            }
        }

        private void OnCastCancelled(Skill skill)
        {
            _skillBtn.interactable = true;
            _cooldownImg.gameObject.SetActive(false);
        }

        private void OnCastStarted(Skill skill)
        {
            _skillBtn.interactable = false;
            _cooldownImg.gameObject.SetActive(true);
            _cooldownImg.fillAmount = 1.0f;
        }

        private void OnStarted()
        {
            _cooldownImg.gameObject.SetActive(true);
        }

        private void OnProgress(float progress)
        {
            _cooldownImg.fillAmount = 1.0f - progress;
        }
        
        private void OnFinished()
        {
            _cooldownImg.gameObject.SetActive(false);
        }

        private void OnSkillClick()
        {
            GameManager.instance.playerUnit.UseSkill(_skill.config.type, GameManager.instance.playerUnit);
        }
    }
}
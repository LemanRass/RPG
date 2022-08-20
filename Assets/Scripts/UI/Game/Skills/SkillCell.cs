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
            _skill.onCooldownStarted += OnCooldownStarted;
            _skill.onCooldownProgress += OnCooldownProgress;
            _skill.onCooldownFinished += OnCooldownFinished;

            _skillImg.sprite = _skill.config.icon;
            _skillImg.gameObject.SetActive(_skill != null);
            _skillBtn.onClick.AddListener(OnSkillClick);
        }

        private void OnCooldownStarted()
        {
            _cooldownImg.gameObject.SetActive(true);
        }

        private void OnCooldownProgress(float progress)
        {
            _cooldownImg.fillAmount = 1.0f - progress;
        }
        
        private void OnCooldownFinished()
        {
            _cooldownImg.gameObject.SetActive(false);
        }

        private void OnSkillClick()
        {
            GameManager.instance.playerUnit.UseSkill(_skill.config.type, GameManager.instance.playerUnit);
        }
    }
}
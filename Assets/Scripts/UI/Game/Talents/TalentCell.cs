using Configs;
using Enums;
using Talents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.Talents
{
    public class TalentCell : MonoBehaviour
    {
        [SerializeField] private Slider _experienceSlider;
        [SerializeField] private TextMeshProUGUI _experienceLabel;
        [SerializeField] private TextMeshProUGUI _levelLabel;

        public TalentType talentType;
        private UnitTalent _talent;
        
        public void Init(UnitTalent talent)
        {
            _talent = talent;
            talent.onExperienceChanged += OnExperienceChanged;
            talent.onLevelChanged += OnLevelChanged;
            Refresh();
        }

        private void OnLevelChanged()
        {
            Refresh();
        }

        private void OnExperienceChanged()
        {
            Refresh();
        }

        private void Refresh()
        {
            int level = _talent.level;
            _levelLabel.text = level.ToString();
            
            int currentExp = _talent.experience;
            int requiredExp = ConfigsManager.talentsLevels[_talent.talentType].experiences[level - 1];

            float levelProgress = Mathf.Clamp01(currentExp / (float)requiredExp);
            _experienceSlider.value = levelProgress;

            _experienceLabel.text = $"{currentExp}/{requiredExp}";
        }
    }
}
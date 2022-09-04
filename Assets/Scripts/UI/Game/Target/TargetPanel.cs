using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.Target
{
    public class TargetPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _titleLabel;
        [Space]
        [SerializeField] private TextMeshProUGUI _healthLabel;
        [SerializeField] private Slider _healthSlider;
        [Space]
        [SerializeField] private TextMeshProUGUI _manaLabel;
        [SerializeField] private Slider _manaSlider;

        private PlayerUnit _playerUnit;
        private Unit _currentTarget;
        
        public void Init()
        {
            _playerUnit = GameManager.instance.playerUnit;
            
            _playerUnit.target.onSelectionChanged += OnTargetChanged;
        }

        private void OnTargetChanged(Unit target)
        {
            if (target != null)
            {
                _currentTarget = target;

                _currentTarget.onHealthChanged += OnTargetHealthChanged;
                _currentTarget.onManaChanged += OnTargetManaChanged;
                _currentTarget.effects.onEffectsChanged += OnTargetEffectsChanged;
                
                Show();
                Refresh();
            }
            else
            {
                _currentTarget.onHealthChanged += OnTargetHealthChanged;
                _currentTarget.onManaChanged += OnTargetManaChanged;
                _currentTarget.effects.onEffectsChanged += OnTargetEffectsChanged;

                _currentTarget = null;
                Hide();
            }
        }


        private void OnTargetHealthChanged(int health) => Refresh();
        private void OnTargetManaChanged(int mana) => Refresh();
        private void OnTargetEffectsChanged() => Refresh();


        private void Show()
        {
            _canvasGroup.alpha = 1.0f;
        }

        private void Hide()
        {
            _canvasGroup.alpha = 0.0f;
        }

        private void Refresh()
        {
            _titleLabel.text = _currentTarget.name;

            int currentHealth = _currentTarget.health;
            int maxHealth = (int)_currentTarget.GetStat(StatType.MAX_HEALTH);
            _healthLabel.text = $"{currentHealth}/{maxHealth}";
            _healthSlider.value = currentHealth / (float)maxHealth;

            int currentMana = _currentTarget.mana;
            int maxMana = (int)_currentTarget.GetStat(StatType.MAX_MANA);
            _manaLabel.text = $"{currentMana}/{maxMana}";
            _manaSlider.value = currentMana / (float)maxMana;
        }
    }
}
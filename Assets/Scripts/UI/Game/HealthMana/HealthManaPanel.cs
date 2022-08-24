using Enums;
using TMPro;
using UnityEngine;

namespace UI.Game.HealthMana
{
    public class HealthManaPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform _healthBar;
        [SerializeField] private TextMeshProUGUI _healthLabel;

        [SerializeField] private RectTransform _manaBar;
        [SerializeField] private TextMeshProUGUI _manaLabel;

        private PlayerUnit _playerUnit;
        
        public void Init()
        {
            _playerUnit = GameManager.instance.playerUnit;

            _playerUnit.onHealthChanged += _ => RefreshHealth();
            _playerUnit.onManaChanged += _ => RefreshMana();
            _playerUnit.equipment.onEquipmentChanged += OnEquipmentChanged;
            _playerUnit.effects.onEffectsChanged += OnEffectsChanged;
            _playerUnit.talents.onTalentsChanged += OnTalentsChanged;
            RefreshHealth();
            RefreshMana();
        }

        private void OnTalentsChanged()
        {
            RefreshHealth();
            RefreshMana();
        }

        private void OnEffectsChanged()
        {
            RefreshHealth();
            RefreshMana();
        }

        private void OnEquipmentChanged()
        {
            RefreshHealth();
            RefreshMana();
        }

        private void RefreshHealth()
        {
            float healthBarWidth = _healthBar.sizeDelta.x;
            float currHealth = _playerUnit.health;
            float maxHealth = _playerUnit.GetStat(StatType.MAX_HEALTH);

            float healthPosX = healthBarWidth * (1.0f - currHealth / maxHealth) * -1;
            _healthBar.anchoredPosition = new Vector2(healthPosX, _healthBar.anchoredPosition.y);

            _healthLabel.text = $"{currHealth}/{maxHealth}";
        }

        private void RefreshMana()
        {
            float manaBarWidth = _manaBar.sizeDelta.x;
            float currMana = _playerUnit.mana;
            float maxMana = _playerUnit.GetStat(StatType.MAX_MANA);

            float manaPosX = manaBarWidth * (1.0f - currMana / maxMana) * -1;
            _manaBar.anchoredPosition = new Vector2(manaPosX, _healthBar.anchoredPosition.y);

            _manaLabel.text = $"{currMana}/{maxMana}";
        }
    }
}
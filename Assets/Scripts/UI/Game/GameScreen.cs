using UI.Game.HealthMana;
using UI.Game.Skills;
using UI.Game.Talents;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private HealthManaPanel _healthManaPanel;
        [Space]
        [SerializeField] private InventoryWidget _inventoryWidget;
        [SerializeField] private Button _inventoryBtn;
        [Space]
        [SerializeField] private SkillsWidget _skillsWidget;
        [SerializeField] private Button _skillsBtn;
        [Space] 
        [SerializeField] private TalentsWidget _talentsWidget;
        [SerializeField] private Button _talentsBtn;

        private void Start()
        {
            _inventoryWidget.Init();
            _inventoryBtn.onClick.AddListener(OnInventoryBtnClick);
            
            _skillsWidget.Init();
            _skillsBtn.onClick.AddListener(OnSkillsBtnClick);
            
            _talentsWidget.Init();
            _talentsBtn.onClick.AddListener(OnTalentsBtnClick);
            
            _healthManaPanel.Init();
        }

        private void OnInventoryBtnClick()
        {
            _inventoryWidget.Show();
        }

        private void OnSkillsBtnClick()
        {
            _skillsWidget.Show();
        }

        private void OnTalentsBtnClick()
        {
            _talentsWidget.Show();
        }
    }
}

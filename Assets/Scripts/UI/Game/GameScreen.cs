using UI.Game.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private InventoryWidget _inventoryWidget;
        [SerializeField] private Button _inventoryBtn;
        [Space]
        [SerializeField] private SkillsWidget _skillsWidget;
        [SerializeField] private Button _skillsBtn;

        private void Start()
        {
            _inventoryWidget.Init();
            _inventoryBtn.onClick.AddListener(OnInventoryBtnClick);
            
            _skillsWidget.Init();
            _skillsBtn.onClick.AddListener(OnSkillsBtnClick);
        }

        private void OnInventoryBtnClick()
        {
            _inventoryWidget.Show();
        }

        private void OnSkillsBtnClick()
        {
            _skillsWidget.Show();
        }
    }
}

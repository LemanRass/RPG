using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.Skills
{
    public class SkillsWidget : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _closeBtn;

        [SerializeField] private SkillsPanel _skillsPanel;

        public void Init()
        {
            _skillsPanel.Init();
        }
        
        public void Show()
        {
            _canvasGroup.alpha = 1.0f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            
            _closeBtn.onClick.AddListener(OnCloseBtnClick);
        }

        public void Hide()
        {
            _closeBtn.onClick.RemoveListener(OnCloseBtnClick);
            
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void OnCloseBtnClick()
        {
            Hide();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.Talents
{
    public class TalentsWidget : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _closeBtn;

        [SerializeField] private TalentsPanel _talentsPanel;

        public void Init()
        {
            _talentsPanel.Init();
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
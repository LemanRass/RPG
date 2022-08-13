using UI.Game.Inventory;
using UI.Game.Inventory.Equipment;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class InventoryWidget : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _closeBtn;

        [SerializeField] private EquipmentPanel _equipmentPanel;
        [SerializeField] private InventoryPanel _inventoryPanel;
        
        public void Init()
        {
            _equipmentPanel.Init();
            _inventoryPanel.Init();
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
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private Button _inventoryBtn;
        [SerializeField] private InventoryWidget _inventoryWidget;

        private void Start()
        {
            _inventoryWidget.Init();
            _inventoryBtn.onClick.AddListener(OnInventoryBtnClick);
        }

        private void OnInventoryBtnClick()
        {
            _inventoryWidget.Show();
        }
    }
}

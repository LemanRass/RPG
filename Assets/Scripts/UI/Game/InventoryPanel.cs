using System.Collections.Generic;
using UnityEngine;

namespace UI.Game
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private InventoryCell _cellPrefab;
        
        private List<InventoryCell> _cells;
        private bool _isInitialized;

        public void Init()
        {
            var playerInventory = GameManager.instance.playerUnit.inventory;
            _cells = new List<InventoryCell>(playerInventory.capacity);

            for (int i = 0; i < playerInventory.capacity; i++)
            {
                var cell = Instantiate(_cellPrefab, _parent);
                cell.gameObject.SetActive(true);
                _cells.Add(cell);
            }

            _isInitialized = true;
        }

        public void Refresh()
        {
            if (!_isInitialized)
                Init();
            
            for (int i = 0; i < _cells.Count; i++)
                _cells[i].Clear();
            
            var playerInventory = GameManager.instance.playerUnit.inventory;
            for (int i = 0; i < playerInventory.itemsCount; i++)
            {
                var slot = playerInventory[i];
                if (slot.isEmpty)
                    continue;
                
                _cells[i].Insert(slot.item);
            }
        }
    }
}
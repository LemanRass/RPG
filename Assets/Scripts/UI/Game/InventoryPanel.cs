using System.Collections.Generic;
using UnityEngine;

namespace UI.Game
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        private List<InventoryCell> _cells;

        private void Start()
        {
            var cells = _parent.GetComponentsInChildren<InventoryCell>();
            _cells = new List<InventoryCell>(cells);
        }

        public void Refresh()
        {
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
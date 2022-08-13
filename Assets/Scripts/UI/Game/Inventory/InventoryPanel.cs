using UnityEngine;

namespace UI.Game.Inventory
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private GameObject _cellPrefab;
        
        public void Init()
        {
            var playerInventory = GameManager.instance.playerUnit.inventory;

            for (int i = 0; i < playerInventory.capacity; i++)
            {
                var cell = Instantiate(_cellPrefab, _parent);
                cell.gameObject.SetActive(true);
                cell.GetComponentInChildren<InventoryCell>().Init(playerInventory[i]);
            }
        }
    }
}
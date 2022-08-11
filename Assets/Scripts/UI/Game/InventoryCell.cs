using Configs.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private Image _itemImg;
        [SerializeField] private TextMeshProUGUI _itemCount;

        public Item item { get; private set; }
        
        public void Insert(Item item)
        {
            this.item = item;

            _itemImg.gameObject.SetActive(true);
            _itemImg.sprite = item.icon;
        }

        public void Clear()
        {
            _itemImg.sprite = null;
            _itemImg.gameObject.SetActive(false);
            item = null;
        }
    }
}
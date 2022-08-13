using System.Collections.Generic;
using Equipment;
using UnityEngine;

namespace UI.Game.Inventory.Equipment
{
    public class EquipmentPanel : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        public void Init()
        {
            var playerEquipment = GameManager.instance.playerUnit.equipment;
            var cells = _parent.GetComponentsInChildren<EquipmentCell>();
            
            for (int i = 0; i < cells.Length; i++)
            {
                var cell = cells[i];
                cell.Init(playerEquipment[cell.equipmentType]);
            }
        }
    }
}
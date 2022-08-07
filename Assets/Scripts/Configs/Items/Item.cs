using Equipment;
using UnityEngine;

namespace Inventory
{
    public enum WeaponType
    {
        SWORD,
        BOW
    }

    public abstract class Weapon : Item
    {
        public WeaponType weaponType;
    }
    
    public abstract class Item : MonoBehaviour
    {
        public EquipmentType type;
    }
}
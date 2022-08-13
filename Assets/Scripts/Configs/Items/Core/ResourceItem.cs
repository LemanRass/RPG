using UnityEngine;

namespace Configs.Items.Core
{
    public class ResourceItem : Item, ICountable
    {
        [SerializeField] private int _count;
        public int count => _count;

        [SerializeField] private int _maxCount;
        public int maxCount => _maxCount;
    }
}
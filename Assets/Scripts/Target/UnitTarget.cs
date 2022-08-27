using System;

namespace Target
{
    public class UnitTarget
    {
        public Unit selected { get; private set; }

        public event Action<Unit> onSelectionChanged; 

        public void Select(Unit unit)
        {
            selected = unit;
            onSelectionChanged?.Invoke(selected);
        }

        public void Clear()
        {
            selected = null;
            onSelectionChanged?.Invoke(selected);
        }
    }
}
using System;
using System.Collections.Generic;
using Enums;

namespace Talents.Configs
{
    [Serializable]
    public class UnitStatConfig
    {
        public StatType type;
        public List<float> levels;
    }
}
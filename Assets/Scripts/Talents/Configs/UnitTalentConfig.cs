using System;
using System.Collections.Generic;
using Enums;

namespace Talents.Configs
{
    [Serializable]
    public class UnitTalentConfig
    {
        public TalentType type;
        public List<UnitStatConfig> stats;
    }
}
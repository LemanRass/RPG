using System;
using System.Collections.Generic;
using Configs.Stats;
using Configs.Talents;
using Enums;

namespace Talents.Configs
{
    [Serializable]
    public class TalentConfig
    {
        public TalentType type;
        public List<StatConfig> stats;
        public List<TalentLevelConfig> levels;
    }
}
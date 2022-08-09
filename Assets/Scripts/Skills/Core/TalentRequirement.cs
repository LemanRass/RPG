using System;
using Enums;

namespace Skills.Core
{
    [Serializable]
    public class TalentRequirement
    {
        public TalentType talentType;
        public int level;

        public bool IsOk(Unit unit)
        {
            return unit.talents[talentType].level >= level;
        }
    }
}
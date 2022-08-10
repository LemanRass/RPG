using System;
using Enums;

namespace Talents.Data
{
    [Serializable]
    public class UnitTalentData
    {
        public TalentType type;
        public int level;
        public int experience;
    }
}
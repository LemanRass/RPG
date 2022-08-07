using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "StatLevelsConfig", menuName = "Unit/Configs/StatLevelsConfig")]
    public class StatLevelsConfig : ScriptableObject
    {
        public StatType statType;
        public TalentType talentType;
        public List<float> values;
    }
}
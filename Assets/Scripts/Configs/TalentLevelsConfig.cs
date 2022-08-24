using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TalentLevelsConfig", menuName = "Unit/Configs/TalentLevelsConfig")]
    public class TalentLevelsConfig : ScriptableObject
    {
        public TalentType talentType;
        public List<int> experiences;
    }
}
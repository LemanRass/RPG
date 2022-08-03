using System.Collections.Generic;
using Talents.Configs;
using UnityEngine;

namespace Configs.Talents
{
    [CreateAssetMenu(fileName = "UnitTalentsConfig", menuName = "Unit/Configs/UnitTalentsConfig")]
    public class TalentsConfig : ScriptableObject
    {
        public List<TalentConfig> talents;
    }
}
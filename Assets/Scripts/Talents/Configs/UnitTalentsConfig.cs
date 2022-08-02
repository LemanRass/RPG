using System.Collections.Generic;
using UnityEngine;

namespace Talents.Configs
{
    [CreateAssetMenu(fileName = "UnitTalentsConfig", menuName = "Unit/Talents/UnitTalentsConfig")]
    public class UnitTalentsConfig : ScriptableObject
    {
        public List<UnitTalentConfig> talents;
    }
}
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Configs.Stats
{
    [CreateAssetMenu(fileName = "UnitStatProgressionConfig", menuName = "Unit/Configs/UnitStatProgressionConfig")]
    public class StatConfig : ScriptableObject
    {
        public StatType type;
        public List<StatLevelConfig> levels;
    }
}
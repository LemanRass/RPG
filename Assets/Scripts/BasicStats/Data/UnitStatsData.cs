using System.Collections.Generic;
using UnityEngine;

namespace BasicStats.Data
{
    [CreateAssetMenu(fileName = "UnitStatsData", menuName = "Unit/Data/UnitStatsData")]
    public class UnitStatsData : ScriptableObject
    {
        public List<UnitStatData> stats;
    }
}
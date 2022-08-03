using System.Collections.Generic;
using UnityEngine;

namespace Talents.Data
{
    [CreateAssetMenu(fileName = "UnitTalentsData", menuName = "Unit/Data/UnitTalentsData")]
    public class UnitTalentsData : ScriptableObject
    {
        public List<UnitTalentData> talents;
    }
}
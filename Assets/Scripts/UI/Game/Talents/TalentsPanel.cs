using System.Linq;
using UnityEngine;

namespace UI.Game.Talents
{
    public class TalentsPanel : MonoBehaviour
    {
        public void Init()
        {
            var cells = GetComponentsInChildren<TalentCell>().ToList();
            
            for (int i = 0; i < GameManager.instance.playerUnit.talents.count; i++)
            {
                var talent = GameManager.instance.playerUnit.talents[i];

                var cell = cells.Find(n => n.talentType == talent.talentType);
                if (cell != null)
                {
                    cell.Init(talent);
                }
            }
        }
    }
}
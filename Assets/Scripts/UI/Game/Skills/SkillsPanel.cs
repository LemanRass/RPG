using System.Collections.Generic;
using UnityEngine;

namespace UI.Game.Skills
{
    public class SkillsPanel : MonoBehaviour
    {
        [SerializeField] private List<SkillCell> _cells;

        public void Init()
        {
            for (int i = 0; i < GameManager.instance.playerUnit.skills.count; i++)
            {
                var skill = GameManager.instance.playerUnit.skills[i];
                _cells[i].Init(skill);
            }
        }
    }
}
using UnityEngine;

namespace Skills.Configs
{
    [CreateAssetMenu(fileName = "AcidSplashSkillConfig", menuName = "Unit/Skills/AcidSplashSkillConfig")]
    public class AcidSplashSkillConfig : SkillConfig
    {
        public float damage;
        public float interval;
        public float duration;
        public float radius;
    }
}
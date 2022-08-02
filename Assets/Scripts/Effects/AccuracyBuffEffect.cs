using Effects.Configs;
using Effects.Core;
using Enums;

namespace Effects
{
    public class AccuracyBuffEffect : Effect
    {
        public AccuracyBuffEffect(AccuracyBuffEffectConfig config)
        {
            statType = StatType.MELEE_ACCURACY;
            duration = config.duration;
            rawValue = config.rawValue;
            percentValue = config.percentValue;
        }
    }
}
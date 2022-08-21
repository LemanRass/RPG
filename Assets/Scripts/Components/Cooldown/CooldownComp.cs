using System;
using UnityEngine;

namespace Components.Cooldown
{
    public class CooldownComp
    {
        public event Action onStarted;
        public event Action<float> onProgress;
        public event Action onFinished;
        
        public float cooldownTicks { get; private set; }
        public float cooldownDuration { get; private set; }
        public float cooldownProgress => cooldownTicks / cooldownDuration;
        public bool isCoolingDown { get; private set; }

        public CooldownComp(float cooldownDuration)
        {
            this.cooldownDuration = cooldownDuration;
        }

        public void Begin()
        {
            cooldownTicks = 0.0f;
            isCoolingDown = true;
            onStarted?.Invoke();
        }

        public void Finish()
        {
            isCoolingDown = false;
            onFinished?.Invoke();
        }

        public virtual void Update()
        {
            if (isCoolingDown)
            {
                cooldownTicks += Time.deltaTime;
                onProgress?.Invoke(cooldownProgress);

                if (Mathf.Abs(cooldownTicks - cooldownDuration) < 0.01f)
                {
                    Finish();
                }
            }
        }
    }
}
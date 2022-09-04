using System;
using Data.Skills;
using UnityEngine;

namespace Views
{
    public class SkillProjectileView : MonoBehaviour
    {
        private Unit _receiver;
        private Action _callback;
        private ProjectileSkill _projectileSkill;

        public void Init(Unit receiver, ProjectileSkill projectileSkill, Action callback = null)
        {
            _receiver = receiver;
            _callback = callback;
            _projectileSkill = projectileSkill;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                _receiver.transform.position + new Vector3(0.0f, 1.0f, 0.0f),
                _projectileSkill.config.projectileSpeed * Time.deltaTime);

            var distance = Vector3.Distance(transform.position, _receiver.transform.position);
            if (distance < 0.5f)
            {
                _callback?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
using UnityEngine;

namespace Views
{
    public class EnemyPointView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particlePrefab;

        private ParticleSystem _particle;

        public Unit currentEnemy { get; private set; }
        
        public void Init()
        {
            _particle = Instantiate(_particlePrefab);
            _particle.Stop();
        }
        
        public void Show(Unit unit)
        {
            currentEnemy = unit;
            _particle.transform.SetParent(unit.transform);
            _particle.transform.localPosition = Vector3.zero;
            _particle.transform.localRotation = Quaternion.identity;
            _particle.transform.localScale = Vector3.one;
            _particle.Play();
        }

        public void Hide()
        {
            currentEnemy = null;
            _particle.transform.parent = null;
            _particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
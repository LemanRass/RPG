using UnityEngine;

namespace Views
{
    public class EnemyPointView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particlePrefab;

        private ParticleSystem _particle;

        private PlayerUnit _playerUnit;
        
        public void Init(PlayerUnit playerUnit)
        {
            _playerUnit = playerUnit;
            _playerUnit.target.onSelectionChanged += OnTargetChanged;
            
            _particle = Instantiate(_particlePrefab);
            _particle.Stop();
        }

        private void OnTargetChanged(Unit target)
        {
            if (target != null)
            {
                Show(target);
            }
            else
            {
                Hide();
            }
        }

        private void Show(Unit unit)
        {
            _particle.transform.SetParent(unit.transform);
            _particle.transform.localPosition = Vector3.zero;
            _particle.transform.localRotation = Quaternion.identity;
            _particle.transform.localScale = Vector3.one;
            _particle.Play();
        }

        private void Hide()
        {
            _particle.transform.parent = null;
            _particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
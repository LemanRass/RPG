using UnityEngine;

namespace Views
{
    public class MovePointView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particlePrefab;

        private ParticleSystem _particle;

        public void Init()
        {
            _particle = Instantiate(_particlePrefab);
            _particle.Stop();
        }
        
        public void Show(Vector3 position)
        {
            _particle.transform.position = position;
            _particle.Play();
        }

        public void Hide()
        {
            _particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
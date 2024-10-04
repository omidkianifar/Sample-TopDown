using System.Collections;
using UnityEngine;


namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class BombController : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 10f;
        [SerializeField] float _bombThrowForce = 10f;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _destroyEffect;

        private void Start()
        {
            StartCoroutine(DestroyAfterTime());
        }

        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(_lifetime);

            Destroy(gameObject);
        }

        public void Throw(Vector3 direction) 
        {
            _rigidbody.AddForce(direction * _bombThrowForce, ForceMode.VelocityChange);
        }

        public void DestroyBomb()
        {
            StopAllCoroutines();

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            var effect = Instantiate(_destroyEffect, transform.position, Quaternion.identity);
        }
    }
}
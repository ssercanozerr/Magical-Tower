using Assets.Scripts.Controllers;
using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Abstract
{
    public abstract class SpellsAbs : MonoBehaviour
    {
        [HideInInspector] public float damage;
        
        private Vector3 _targetPosition;
        private float _speed;

        public void Initialize(Vector3 target, float speed, float damage)
        {
            _targetPosition = target;
            _speed = speed;
            this.damage = damage;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Beaver") || other.gameObject.CompareTag("Ghost") || other.gameObject.CompareTag("Golem"))
            {
                EnemyDetected(other);
            }
        }

        public virtual void EnemyDetected(Collider other)
        {
            PoolSignals.Instance.onObjReturnToPool(gameObject.tag, gameObject);
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }
}
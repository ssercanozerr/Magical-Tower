using Assets.Scripts.Abstract;
using Assets.Scripts.Controllers;
using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Handlers
{
    public class FireballHandler : SpellsAbs
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask layerMask;

        public override void EnemyDetected(Collider other)
        {
            PoolSignals.Instance.onObjReturnToPool(gameObject.tag, gameObject);
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
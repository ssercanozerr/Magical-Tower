using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Signals;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyInfos enemyInfos;
        [SerializeField] private Image healthImage;

        private float _health;
        private float _speed;
        private float _attackDistance;
        private Transform _tower;
        private Animator _animator;

        private void Awake()
        {
            _health = enemyInfos.health;
            healthImage.fillAmount = _health / enemyInfos.health;
        }

        private void Start()
        {
            GameObject tower = GameObject.FindWithTag("Tower");
            
            _tower = tower.transform;
            _speed = enemyInfos.speed;
            _attackDistance = enemyInfos.attackDistance;
            
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            float distanceToTower = Vector3.Distance(transform.position, _tower.position);

            if (distanceToTower <= _attackDistance) { TriggerAttack(); }
            else { MoveTowardsTower(); }
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            _health = Mathf.Clamp(_health, 0, enemyInfos.health);
            healthImage.fillAmount = _health / enemyInfos.health;

            if (_health <= 0)
            {
                _health = enemyInfos.health;
                healthImage.fillAmount = _health / enemyInfos.health;
                PoolSignals.Instance.onObjReturnToPool?.Invoke(gameObject.tag, gameObject);
                GameSignals.Instance.onReturnEnemies?.Invoke().Remove(gameObject);
                ScoreSignals.Instance.onUpdateScore?.Invoke(enemyInfos.score);
            }
        }

        private void MoveTowardsTower()
        {
            Vector3 direction = (_tower.position - transform.position).normalized;
            transform.position += _speed * Time.deltaTime * direction;
        }

        private void TriggerAttack()
        {
            _animator.SetBool("Attack", true);            
        }

        private void HitDamage()
        {
            TowerSignals.Instance.onDecreaseHealth?.Invoke(enemyInfos.damage);
        }
    }
}

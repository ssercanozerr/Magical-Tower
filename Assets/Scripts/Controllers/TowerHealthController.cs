using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class TowerHealthController : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private Image healthImage;

        private float _health;

        private void Awake()
        {
            _health = maxHealth;
            healthImage.fillAmount = _health / maxHealth;
        }

        public void OnDecreaseHealth(float damage)
        {
            _health -= damage;
            _health = Mathf.Clamp(_health, 0, maxHealth);
            healthImage.fillAmount = _health / maxHealth;

            if (_health <= 0)
            {
                GameSignals.Instance.onGameOver?.Invoke();
            }
        }
    }
}
using Assets.Scripts.Controllers;
using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private TowerHealthController towerHealthController;

        private void OnEnable()
        {
            TowerSignals.Instance.onDecreaseHealth += towerHealthController.OnDecreaseHealth;
        }

        private void OnDisable()
        {
            TowerSignals.Instance.onDecreaseHealth -= towerHealthController.OnDecreaseHealth;
        }
    }
}
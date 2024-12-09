using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Signals
{
    public class TowerSignals : MonoBehaviour
    {
        public static TowerSignals Instance;

        public UnityAction<float> onDecreaseHealth;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Signals
{
    public class ScoreSignals : MonoBehaviour
    {
        public static ScoreSignals Instance;

        public UnityAction<int> onUpdateScore;

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
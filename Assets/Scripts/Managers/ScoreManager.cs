using Assets.Scripts.Controllers;
using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private ScoreController scoreController;

        private void OnEnable()
        {
            ScoreSignals.Instance.onUpdateScore += scoreController.OnUpdateScore;
        }

        private void OnDisable()
        {
            ScoreSignals.Instance.onUpdateScore -= scoreController.OnUpdateScore;
        }
    }
}
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI bestScoreText;

        private int _score;
        private int _bestScore;

        private void Awake()
        {
            _score = 0;
            _bestScore = PlayerPrefs.GetInt("BestScore", 0);
            UpdateBestScore();
        }

        public void OnUpdateScore(int score)
        {
            _score += score;
            scoreText.text = "Score : " + _score.ToString();

            if (_score > _bestScore)
            {
                _bestScore = _score;
                PlayerPrefs.SetInt("BestScore", _bestScore);
                UpdateBestScore();
            }
        }

        private void UpdateBestScore()
        {
            bestScoreText.text = _bestScore.ToString();
            PlayerPrefs.Save();
        }
    }
}
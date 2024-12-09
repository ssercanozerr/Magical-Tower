using Assets.Scripts.Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform tower;
        [SerializeField] private float spawnRadius;
        [SerializeField] private Vector3 spawnRadiusOffset;

        [SerializeField] private float waveInterval;
        [SerializeField] private float spawnInterval;
        [SerializeField] private int initialEnemyCount;

        [SerializeField] private GameObject endPanel;

        private int _currentWave = 0;
        private int _currentEnemyCount;
        private List<GameObject> _enemies = new();

        private void Start()
        {
            StartNextWave();
        }

        public void OnGameOver()
        {
            Time.timeScale = 0f;
            endPanel.SetActive(true);
        }

        public List<GameObject> OnReturnEnemies()
        {
            return _enemies;
        }

        private void StartNextWave()
        {
            _currentEnemyCount = initialEnemyCount + _currentWave;
            _currentWave++;
            StartCoroutine(SpawnEnemiesWave());
        }

        private IEnumerator SpawnEnemiesWave()
        {
            for (int i = 0; i < _currentEnemyCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(waveInterval);
            StartNextWave();
        }

        private void SpawnEnemy()
        {
            float angle = Random.Range(0f, 360f);
            float radians = angle * Mathf.Deg2Rad;

            Vector3 spawnPosition = tower.position + spawnRadiusOffset + (new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * spawnRadius);

            string enemyType = GetRandomEnemyType();
            GameObject enemy = PoolSignals.Instance.onGetObjFromPool?.Invoke(enemyType);

            enemy.transform.position = spawnPosition;
            enemy.transform.LookAt(tower);
            _enemies.Add(enemy);  
        }

        private string GetRandomEnemyType()
        {
            string[] enemyTypes = { "Beaver", "Ghost", "Golem" };
            int randomIndex = Random.Range(0, enemyTypes.Length);
            return enemyTypes[randomIndex];
        }
    }
}
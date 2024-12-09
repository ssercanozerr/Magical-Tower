using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemyInfos", menuName = "Self/Enemy Infos")]
    public class EnemyInfos : ScriptableObject
    {
        public float speed;
        public float health;
        public float damage;
        public float attackDistance;
        public int score;
    }
}
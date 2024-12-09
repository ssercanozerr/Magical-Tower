using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewSpellInfos", menuName = "Self/Spell Infos")]
    public class SpellInfos : ScriptableObject
    {
        public float speed;
        public float damage;
        public float coolDown;
    }
}
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Signals
{
    public class PoolSignals : MonoBehaviour
    {
        public static PoolSignals Instance;

        public Func<string, GameObject> onGetObjFromPool;
        public UnityAction<string, GameObject> onObjReturnToPool;

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
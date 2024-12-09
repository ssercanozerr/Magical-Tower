using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Signals
{
    public class GameSignals : MonoBehaviour
    {
        public static GameSignals Instance;

        public Func<List<GameObject>> onReturnEnemies;
        public UnityAction onGameOver;

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
using Assets.Scripts.Controllers;
using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private PoolController poolController;

        private void OnEnable()
        {
            PoolSignals.Instance.onGetObjFromPool += poolController.OnGetObjFromPool;
            PoolSignals.Instance.onObjReturnToPool += poolController.OnObjReturnToPool;
        }

        private void OnDisable()
        {
            
            PoolSignals.Instance.onGetObjFromPool -= poolController.OnGetObjFromPool;
            PoolSignals.Instance.onObjReturnToPool -= poolController.OnObjReturnToPool;
        }
    }
}

using Script.Ahmet.ObjectPool;
using System.Collections;
using UnityEngine;

namespace Assets.Script.Runtime
{
    public class GameObjectReturnToPool : MonoBehaviour
    {
        [SerializeField]
        private float returnToPoolDelayTime; 

        private WaitForSeconds waitForReturnDelay;

        private void Awake()
        {
            waitForReturnDelay = new WaitForSeconds(returnToPoolDelayTime);
        }

        private void OnEnable()
        {
            StartCoroutine(ReturnToPoolAfterDelay());
        }

        private IEnumerator ReturnToPoolAfterDelay()
        {
            yield return waitForReturnDelay;
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}

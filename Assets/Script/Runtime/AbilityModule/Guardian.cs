using UnityEngine;

namespace Assets.Script.Runtime.AbilityModule
{
    public class Guardian : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] guardianParticleArray;

        private float _spinSpeed;

        public void SetSpeed(float spinSpeed)
        {
            _spinSpeed = spinSpeed;
        }

        private void Update()
        {
            transform.Rotate(0, 0, _spinSpeed * Time.deltaTime);
        }

        public void ActivateParticle(int level)
        {
            guardianParticleArray[level].SetActive(true);
        }
    }
}
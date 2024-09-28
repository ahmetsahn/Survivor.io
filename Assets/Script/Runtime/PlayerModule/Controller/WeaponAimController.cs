using Script.Runtime.PlayerModule.View;
using UnityEngine;
using Zenject;

namespace Script.Runtime.PlayerModule.Controller
{
    public class PlayerWeaponAimController : ITickable
    {
        private readonly PlayerView _view;
        
        private readonly Camera _camera;

        public PlayerWeaponAimController(PlayerView view)
        {
            _view = view;
            _camera = Camera.main;
        }
        
        public void Tick()
        {
            UpdateAimAndScale();
        }

        private void UpdateAimAndScale()
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            Vector3 aimDirection = (mousePosition - _view.transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            _view.AimTransform.eulerAngles = new Vector3(0, 0, angle);
            Vector3 aimLocalScale = Vector3.one;
            
            if (angle > 90 || angle < -90)
            {
                _view.BodyTransform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
                aimLocalScale.y = -1f;
            }
            
            else
            {
                _view.BodyTransform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                aimLocalScale.y = +1f;
            }
            
            _view.AimTransform.localScale = aimLocalScale;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, _camera);
            vec.z = 0f;
            return vec;
        }
        
        private Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            return worldCamera.ScreenToWorldPoint(screenPosition);
        }
    }
}
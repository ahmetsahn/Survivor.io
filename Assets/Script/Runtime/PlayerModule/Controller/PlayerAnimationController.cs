using System.Collections;
using UnityEngine;
using Script.Runtime.PlayerModule.View;
using Script.Runtime.PlayerModule.Model;
using System;

namespace Assets.Script.Runtime.PlayerModule.Controller
{
    public class PlayerAnimationController : IDisposable
    {
        private readonly PlayerView _view;

        private readonly PlayerModel _model;

        public PlayerAnimationController(PlayerView view, PlayerModel model)
        {
            _view = view;
            _model = model;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _view.OnMoveEvent += PlayMoveAnimation;
        }

        private void CrossFadeAnimation(int animationHash, float fadeTime = 0f)
        {
            _view.Animator.CrossFade(animationHash, fadeTime);
        }

        private void PlayMoveAnimation(Vector2 moveDirection)
        {
            CrossFadeAnimation(moveDirection != Vector2.zero ? _model.AnimationHashMove : _model.AnimationHashIdle);
        }

        private void UnsubscribeEvents()
        {
            _view.OnMoveEvent -= PlayMoveAnimation;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
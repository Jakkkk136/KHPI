using System;
using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.UI
{
    public class OpenShopButton : BaseButton
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _atentionGameObject;
        
        private bool _playerOpenShopFirstTime;
        
        private void OnEnable()
        {
            _playerOpenShopFirstTime = PlayerPrefs.HasKey("PlayerOpenShopFirstTime");

            if (_playerOpenShopFirstTime == false)
            {
                _animator.enabled = true;
            }
        }

        protected override void OnClick()
        {
            base.OnClick();
            
            _animator.enabled = false;
            _atentionGameObject.SetActive(false);
            PlayerPrefs.SetInt("PlayerOpenShopFirstTime", 1);
            UIManager.Instance.ShowWindow(eWindowType.Shop);
        }
    }
}
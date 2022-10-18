using _Scripts.Enums;
using _Scripts.Patterns.Events;
using UnityEngine;

namespace _Scripts.UI
{
	public sealed class OpenUpgradeWindowButton : BaseButton
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private GameObject _atentionGameObject;

		private bool _playerOpenUpgradeWindowFirstTime;

		private void OnEnable()
		{
			_playerOpenUpgradeWindowFirstTime = PlayerPrefs.HasKey("PlayerOpenUpgradeWindowFirstTime");

			if (_playerOpenUpgradeWindowFirstTime == false)
			{
				_animator.enabled = true;
			}
		}

		protected override void OnClick()
		{
			base.OnClick();
			
			this.OnEvent(EventID.UPGRADE_WINDOW_OPENED);
			
			_animator.enabled = false;
			_atentionGameObject.SetActive(false);
			PlayerPrefs.SetInt("PlayerOpenUpgradeWindowFirstTime", 1);
			UIManager.Instance.ShowWindow(eWindowType.ItemsUI);
		}
	}
}
using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.UI
{
    public sealed class SettingsButton : BaseButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            
            Time.timeScale = 0f;
            UIManager.Instance.ShowWindow(eWindowType.Settings);
        }
    }
}



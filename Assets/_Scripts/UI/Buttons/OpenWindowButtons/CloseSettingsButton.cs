using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.UI
{
    public class CloseSettingsButton : BaseButton
    {
        protected override void OnClick()
        {
            base.OnClick();

            Time.timeScale = 1f;
            UIManager.Instance.ShowWindow(UIManager.previousWindowType);
        }
    }
}
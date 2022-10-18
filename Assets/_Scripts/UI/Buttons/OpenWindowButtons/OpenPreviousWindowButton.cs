using UnityEngine;

namespace _Scripts.UI
{
    public class OpenPreviousWindowButton : BaseButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            
            UIManager.Instance.OpenPreviousWindow();
        }
    }
}
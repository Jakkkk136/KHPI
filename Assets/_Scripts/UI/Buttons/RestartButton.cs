using _Scripts.Controllers;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace _Scripts.UI
{
    public sealed class RestartButton : BaseButton
    {
        protected override void OnClick()
        {
            base.OnClick();

            DOTween.KillAll();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);            
        }
    }
}

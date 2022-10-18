using _Configs.ScriptableObjectsDeclarations.Configs;
using _Scripts.Controllers;
using _Scripts.Helpers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.UI
{
    public sealed class NextLevelButton : BaseButton
    {
        private bool pressed = false;

        
        protected override void OnClick()
        {
            base.OnClick();
            
            SetNextLevel(); 
        }

        private void SetNextLevel()
        {
            if(pressed) return;
            pressed = true;
            
            
            DOTween.KillAll();
            
            DelayAction.Instance.WaitForSeconds(
                    () =>
                    {
                        SceneManager.LoadScene(LevelOrder.Instance.GetLevelConfig(SaveManager.LevelForPlayer).scene); 
                    }, OtherSettingsSO.Instance.DelayLoadingNextLevel);
        }
    }
}

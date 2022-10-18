using _Scripts.Helpers;
using _Scripts.UI;
using UnityEngine;

public sealed class WinWindow : UIWindow
{
    [SerializeField] private CanvasConfig config;
    [SerializeField] private GameObject nextLevelButton;

    private bool delayedOnce;

    private void OnEnable()
    {
        if (delayedOnce == false)
        {
            nextLevelButton.SetActive(false);
            DelayAction.Instance.WaitForSeconds(() => nextLevelButton.SetActive(true), config.DelayShowingNextLevelButton);

            delayedOnce = true;
        }
    }
}

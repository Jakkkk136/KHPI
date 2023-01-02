using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideWindowButton : BaseButton
{
    [SerializeField] private GameObject windowToShow;
    [SerializeField] private GameObject[] associatedWindowsToControl;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private string activeStateText, inactiveStateText;

    private void OnEnable()
    {
        statusText.text = windowToShow.activeInHierarchy ? activeStateText : inactiveStateText;
    }

    protected override void OnClick()
    {
        base.OnClick();

        windowToShow.SetActive(!windowToShow.activeInHierarchy);

        bool disable = windowToShow.activeInHierarchy == false;

        if (disable)
        {
            foreach (GameObject o in associatedWindowsToControl)
            {
                o.SetActive(false);
            }
        }

        statusText.text = windowToShow.activeInHierarchy ? activeStateText : inactiveStateText;
    }
}

using _Scripts.Controllers;
using _Scripts.UI;

public class MainMenuButton : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        
        Loader.Instance.OpenMainMenu();
    }
}

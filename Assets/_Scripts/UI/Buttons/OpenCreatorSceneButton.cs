using _Scripts.Controllers;
using _Scripts.UI;

public class OpenCreatorSceneButton : BaseButton
{
	protected override void OnClick()
	{
		base.OnClick();
		
		Loader.Instance.OpenCreatorScene();
	}
}

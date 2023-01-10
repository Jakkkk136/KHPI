using _Scripts.Controllers;

namespace _Scripts.UI
{
	public class OpenLevelFileAndEditButton : CanvasSampleOpenFileText
	{
		protected override void OnLevelFileOpened()
		{
			base.OnLevelFileOpened();

			CreatorWindow.Instance.EditExistingLevelConfig();
		}
	}
}
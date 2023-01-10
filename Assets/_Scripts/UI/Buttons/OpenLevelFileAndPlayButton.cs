using _Scripts.Controllers;

namespace _Scripts.UI
{
	public class OpenLevelFileAndPlayButton : CanvasSampleOpenFileText
	{
		protected override void OnLevelFileOpened()
		{
			base.OnLevelFileOpened();

			Loader.Instance.LoadLevel();
		}
	}
}
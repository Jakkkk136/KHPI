namespace _Scripts.Patterns.Events.OnUnityEvents
{
	public class EventOnDisable : OnUnityEventBase
	{
		private void OnDisable()
		{
			eventToCall?.Invoke();
		}
	}
}
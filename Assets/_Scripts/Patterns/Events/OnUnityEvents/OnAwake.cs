namespace _Scripts.Patterns.Events.OnUnityEvents
{
    public sealed class OnAwake : OnUnityEventBase
    {
        private void Awake()
        {
            eventToCall?.Invoke();
        }
    }
}
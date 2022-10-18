using System.Collections.Generic;

namespace _Scripts.Patterns.Events
{
    public sealed class EventController
    {
        private List<ListenerData> _listeners = new List<ListenerData>();
        
        public void RegisterListener(ListenerData listenerData)
        {
            _listeners.Add(listenerData);
        }

        public void RemoveListener(ListenerData listenerData)
        {
            _listeners.Remove(listenerData);
        }

        public void Call(object param)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].Callback.Call(param);
            }
        }
    }
}

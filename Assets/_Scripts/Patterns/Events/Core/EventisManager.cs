using System;
using Object = System.Object;

namespace _Scripts.Patterns.Events
{
    [AutoCreateSingelton]
    public sealed class EventisManager : Singleton<EventisManager>
    {
        private EventisInternalManager _manager;

        private void Awake()
        {
            _manager = new EventisInternalManager();
        }
        
        public void Subscribe(Object listener, Action callback, string eventID)
        {
            EventCallbackData callbackData = new EventCallbackData(callback);
            Subscribe(listener, callbackData, eventID);
        }

        public void Subscribe<T>(Object listener, Action<T> typedCallback, string eventID)
        {
            EventTypedCallbackData<T> callbackData = new EventTypedCallbackData<T>(typedCallback);
            Subscribe(listener, callbackData, eventID);
        }

        public void Unsubscribe(Object listener, Action callback, string eventID)
        {
            EventCallbackData callbackData = new EventCallbackData(callback);
            Unsubscribe(listener, callbackData, eventID);
        }

        public void Unsubscribe<T>(Object listener, Action<T> typedCallback, string eventID)
        {
            EventTypedCallbackData<T> callbackData = new EventTypedCallbackData<T>(typedCallback);
            Unsubscribe(listener, callbackData, eventID);
        }

        private void Subscribe(Object listener, BaseCallbackData callbackData, string eventID)
        {
            ListenerData listenerData = new ListenerData(listener, callbackData, eventID);
            
            _manager.Subscribe(listenerData);
        }
        
        private void Unsubscribe(Object listener, BaseCallbackData callbackData, string eventID)
        {
            ListenerData listenerData = new ListenerData(listener, callbackData, eventID);
            
            _manager.Unsubscribe(listenerData);
        }

        public void OnEvent(string eventID)
        {
            _manager.OnEvent(eventID);
        }
        
        public void OnEvent<T>(string eventID, T param)
        {
            _manager.OnEvent(eventID, param);
        }
        
    }
}

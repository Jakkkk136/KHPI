using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Patterns.Events
{
    public sealed class EventisInternalManager
    {
        private Dictionary<string, EventController> _eventControllers;

        public EventisInternalManager()
        {
            _eventControllers = new Dictionary<string, EventController>();

            foreach (string eventName in EventID.GetAllEventsNames().ToList())
            {
                _eventControllers.Add(eventName, new EventController());
            }
        }

        public void Subscribe(ListenerData listenerData)
        {
            _eventControllers[listenerData.EventID].RegisterListener(listenerData);
        }

        public void Unsubscribe(ListenerData listenerData)
        {
            _eventControllers[listenerData.EventID].RemoveListener(listenerData);
        }

        public void OnEvent(string eventID, object param = null)
        {
            _eventControllers[eventID].Call(param);
        }

    }
}

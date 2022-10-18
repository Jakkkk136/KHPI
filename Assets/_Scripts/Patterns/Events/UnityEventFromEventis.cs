using System.Collections.Generic;
using _Scripts.Patterns.Events;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public sealed class UnityEventFromEventis : MonoBehaviour
{
    public UnityEvent UnityEvent;
    
    [ValueDropdown("GetAllEventsNames")] 
    [SerializeField] private string eventId;
        
    public IEnumerable<string> GetAllEventsNames()
    {
        return EventID.GetAllEventsNames();
    }

    private void OnEnable()
    {
        this.Subscribe(eventId, UnityEvent.Invoke);
    }

    private void OnDestroy()
    {
        this.Unsubscribe(eventId, UnityEvent.Invoke);
    }
}

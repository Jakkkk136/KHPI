using System.Collections.Generic;
using _Scripts.Patterns.Events;
using Sirenix.OdinInspector;
using UnityEngine;

[TypeInfoBox("Choose event name  \n\n " +
             "then pass this object to any UnityEvent and choose public function to call picked event")]
public sealed class EventisEventCaller : MonoBehaviour
{
    [ValueDropdown("GetAllEventsNames")] 
    public string eventId;
        
    public IEnumerable<string> GetAllEventsNames()
    {
        return EventID.GetAllEventsNames();
    }
    
    public void CallEventisEvent()
    {
        this.OnEvent(eventId);
    }
}




using System.Collections.Generic;
using System.Linq;
using _Scripts.Patterns.Events;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Utils.Camera
{
    public sealed class ChangeCameraOnEvent : MonoBehaviour
    {
        private static bool inited;
        private static List<CinemachineVirtualCamera> camerasOnLevel = new List<CinemachineVirtualCamera>();
        
        [SerializeField] private CinemachineVirtualCamera camTo;

        [ValueDropdown("GetAllEventsNames")] 
        [SerializeField] private string eventId;
        
        public IEnumerable<string> GetAllEventsNames()
        {
            return EventID.GetAllEventsNames();
        }

        private void Awake()
        {
            if (inited) return;
            inited = true;
            camerasOnLevel = FindObjectsOfType<CinemachineVirtualCamera>().ToList();
        }

        private void OnEnable()
        {
            this.Subscribe(eventId, ChangeCamsInstant);
        }
        

        private void OnDestroy()
        {
            inited = false;
            
            this.Unsubscribe(eventId, ChangeCamsInstant);
        }

        
        private void ChangeCamsInstant()
        {
            CinemachineVirtualCamera camFrom = camerasOnLevel.Aggregate((x, y) => x.Priority >= y.Priority ? x : y);
            
            if(camFrom.Priority < camTo.Priority) return;
            
            (camFrom.Priority, camTo.Priority) = (camTo.Priority, camFrom.Priority);
        }
    }
}

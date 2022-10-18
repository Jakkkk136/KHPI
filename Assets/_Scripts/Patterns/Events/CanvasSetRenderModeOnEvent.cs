using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Patterns.Events
{
    public class CanvasSetRenderModeOnEvent : MonoBehaviour
    {
        [ValueDropdown("GetAllEventsNames")] 
        [SerializeField] public string eventId;
        [SerializeField] private RenderMode canvasRenderMode;

        private Canvas canvas;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            
            this.Subscribe(eventId, SetRenderMode);
        }

        private void OnDestroy()
        {
            this.Unsubscribe(eventId, SetRenderMode);
        }

        private void SetRenderMode()
        {
            canvas.renderMode = canvasRenderMode;
        }
        
        public IEnumerable<string> GetAllEventsNames()
        {
            return EventID.GetAllEventsNames();
        }
    }
}